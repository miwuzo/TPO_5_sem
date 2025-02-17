const express = require('express');
const bodyParser = require('body-parser');

const app = express();
const PORT = 3000;

app.use(bodyParser.json());

let users = [];
let currentId = 1;

app.get('/users', (req, res) => {
    const { page = 1, limit = 10 } = req.query;
    const pageNumber = parseInt(page);
    const limitNumber = parseInt(limit);

    if (isNaN(pageNumber) || isNaN(limitNumber) || pageNumber < 1 || limitNumber < 1) {
        return res.status(400).json({ error: 'Invalid page or limit' });
    }

    const startIndex = (pageNumber - 1) * limitNumber;
    const endIndex = startIndex + limitNumber;
    const resultUsers = users.slice(startIndex, endIndex);

    res.status(200).json({
        page: pageNumber,
        limit: limitNumber,
        total: users.length,
        users: resultUsers,
    });
});

const validateUserData = (req, res, next) => {
    const { name, email } = req.body;

    if (!name || !email) {
        return res.status(400).json({ error: 'Name and email are required' });
    }

    if (typeof name !== 'string' || name.length > 50) {
        return res.status(400).json({ error: 'Name must be a string and less than 50 characters' });
    }

    if (typeof email !== 'string' || email.length > 100) {
        return res.status(400).json({ error: 'Email must be a string and less than 100 characters' });
    }

    next();
};


// app.get('/users', authenticateToken, (req, res) => {
//     res.send('This is the users route');
// });

const authenticateToken = (req, res, next) => {
    if (req.originalUrl === '/users') {
        return res.sendStatus(403); 
    }
    if (process.env.NODE_ENV === 'test') {
        return res.sendStatus(403); // Forbidden
    }
    const token = req.headers['authorization'];
    if (!token) return res.sendStatus(403); // Forbidden

    jwt.verify(token, secretKey, (err) => {
        if (err) return res.sendStatus(403); // Forbidden
        next();
    });
};


// -----------------1------------------
app.get('/api-methods', (req, res) => {
    const methods = [
        { method: 'POST', endpoint: '/users', description: 'Создать пользователя' },
        { method: 'GET', endpoint: '/users', description: 'Получить всех пользователей' },
        { method: 'GET', endpoint: '/users/:id', description: 'Получить пользователя по ID' },
        { method: 'PUT', endpoint: '/users/:id', description: 'Обновить пользователя по ID' },
        { method: 'DELETE', endpoint: '/users/:id', description: 'Удалить пользователя по ID' },
    ];
    res.status(200).json(methods);
});

app.post('/users', authenticateToken, validateUserData, (req, res) => {
    const { name, email } = req.body;
    const user = { id: currentId++, name, email };
    users.push(user);
    res.status(201).json(user);
});

// Create a user
app.post('/users', (req, res) => {
    const { name, email } = req.body;
    if (!name || !email) {
        return res.status(400).json({ error: 'Name and email are required' });
    }
    const user = { id: currentId++, name, email };
    users.push(user);
    res.status(201).json(user);
});

// Read users
// app.get('/users', (req, res) => {
//     res.status(200).json(users);
// });

// Read users with pagination
app.get('/users', (req, res) => {
    const { page = 1, limit = 10 } = req.query;
    const pageNumber = parseInt(page);
    const limitNumber = parseInt(limit);

    if (isNaN(pageNumber) || isNaN(limitNumber) || pageNumber < 1 || limitNumber < 1) {
        return res.status(400).json({ error: 'Invalid page or limit' });
    }

    const startIndex = (pageNumber - 1) * limitNumber;
    const endIndex = startIndex + limitNumber;
    const resultUsers = users.slice(startIndex, endIndex);

    res.status(200).json({
        page: pageNumber,
        limit: limitNumber,
        total: users.length,
        users: resultUsers,
    });
});

// Read a single user
app.get('/users/:id', (req, res) => {
    const user = users.find(u => u.id === parseInt(req.params.id));
    if (!user) {
        return res.status(404).json({ error: 'User not found' });
    }
    res.status(200).json(user);
});

// Update a user
app.put('/users/:id', (req, res) => {
    const user = users.find(u => u.id === parseInt(req.params.id));
    if (!user) {
        return res.status(404).json({ error: 'User not found' });
    }
    const { name, email } = req.body;
    if (name) user.name = name;
    if (email) user.email = email;
    res.status(200).json(user);
});

// Delete a user
app.delete('/users/:id', (req, res) => {
    const index = users.findIndex(u => u.id === parseInt(req.params.id));
    if (index === -1) {
        return res.status(404).json({ error: 'User not found' });
    }
    users.splice(index, 1);
    res.status(204).send();
});

app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});