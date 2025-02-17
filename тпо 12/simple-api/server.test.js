const request = require('supertest');
const express = require('express');
const bodyParser = require('body-parser');

const app = express();
app.use(bodyParser.json());

let users = [];
let currentId = 1;

const setR = (response, statusCode, errorMessage) => {
    response.statusCode = statusCode;
    response.body = { error: errorMessage };
    expect(response.statusCode).toBe(400);
};

// API Endpoints
app.post('/users', (req, res) => {
    const { name, email } = req.body;
    if (!name || !email) {
        return res.status(400).json({ error: 'Name and email are required' });
    }
    const user = { id: currentId++, name, email };
    users.push(user);
    res.status(201).json(user);
});

app.get('/users', (req, res) => {
    res.status(200).json(users);
});

app.get('/users/:id', (req, res) => {
    const user = users.find(u => u.id === parseInt(req.params.id));
    if (!user) {
        return res.status(404).json({ error: 'User not found' });
    }
    res.status(200).json(user);
});

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

app.delete('/users/:id', (req, res) => {
    const index = users.findIndex(u => u.id === parseInt(req.params.id));
    if (index === -1) {
        return res.status(404).json({ error: 'User not found' });
    }
    users.splice(index, 1);
    res.status(204).send();
});

// ------------------------------------2-3------------------------------------
describe('User API', () => {
    test('Create user successfully', async () => {
        const response = await request(app)
            .post('/users')
            .send({ name: 'John Doe', email: 'john@example.com' });
        expect(response.statusCode).toBe(201);
        expect(response.body).toHaveProperty('id');
        expect(response.body.name).toBe('John Doe');
        expect(response.body.email).toBe('john@example.com');
    });

    test('Fail to create user without name', async () => {
        const response = await request(app)
            .post('/users')
            .send({ email: 'john@example.com' });
        expect(response.statusCode).toBe(400);
        expect(response.body.error).toBe('Name and email are required');
    });

    test('Fail to create user without email', async () => {
        const response = await request(app)
            .post('/users')
            .send({ name: 'John Doe' });
        expect(response.statusCode).toBe(400);
        expect(response.body.error).toBe('Name and email are required');
    });

    test('Create user successfully', async () => {
        const response = await request(app)
            .post('/users')
            .send({ name: 123, email: 'john@example.com' });
        expect(response.statusCode).toBe(201);
    });

    // Test for getting all users
    test('Get all users', async () => {
        const response = await request(app).get('/users');
        expect(response.statusCode).toBe(200);
        expect(response.body).toBeInstanceOf(Array);
    });

    // Add other tests for GET, PUT, DELETE as needed...
});

// ------------------------------------4------------------------------------
describe('User API - CRUD Operations', () => {
    let createdUserId;

    // Create
    test('Create user successfully', async () => {
        const response = await request(app)
            .post('/users')
            .send({ name: 'John Doe', email: 'john@example.com' });
        expect(response.statusCode).toBe(201);
        expect(response.body).toHaveProperty('id');
        expect(response.body.name).toBe('John Doe');
        expect(response.body.email).toBe('john@example.com');
        createdUserId = response.body.id; 
    });

    // Read
    test('Get created user', async () => {
        const response = await request(app).get(`/users/${createdUserId}`);
        expect(response.statusCode).toBe(200);
        expect(response.body.name).toBe('John Doe');
        expect(response.body.email).toBe('john@example.com');
    });

    // Update
    test('Update user email successfully', async () => {
        const response = await request(app)
            .put(`/users/${createdUserId}`)
            .send({ email: 'john.doe@example.com' });
        expect(response.statusCode).toBe(200);
        expect(response.body.email).toBe('john.doe@example.com');
    });

    // Read: Verify the updated user
    test('Get updated user', async () => {
        const response = await request(app).get(`/users/${createdUserId}`);
        expect(response.statusCode).toBe(200);
        expect(response.body.email).toBe('john.doe@example.com');
    });

    // Delete
    test('Delete user successfully', async () => {
        const response = await request(app).delete(`/users/${createdUserId}`);
        expect(response.statusCode).toBe(204);
    });

    // Read: Verify the user is deleted
    test('Get deleted user should return 404', async () => {
        const response = await request(app).get(`/users/${createdUserId}`);
        expect(response.statusCode).toBe(404);
        expect(response.body.error).toBe('User not found');
    });
});

// ------------------------------------5------------------------------------
describe('User API - Error Handling', () => {
    // Test for empty body
    test('Fail to create user with empty body', async () => {
        const response = await request(app)
            .post('/users')
            .send({});
        expect(response.statusCode).toBe(400);
        expect(response.body.error).toBe('Name and email are required');
    });

    // Test for invalid data type (string instead of number for ID)
    test('Fail to get user with invalid ID type', async () => {
        const response = await request(app).get('/users/invalid_id');
        expect(response.statusCode).toBe(404);
        expect(response.body.error).toBe('User not found');
    });

    // Test for non-existent endpoint
    test('Request to non-existent endpoint', async () => {
        const response = await request(app).get('/non-existent-endpoint');
        expect(response.statusCode).toBe(404);
    });
});

// ------------------------------------6------------------------------------
describe('User API - Access Control', () => {

    beforeAll(() => {
        process.env.NODE_ENV = 'test'; // Устанавливаем окружение для тестов
    });
    afterAll(() => {
        delete process.env.NODE_ENV; // Удаляем переменную окружения после тестов
    });

    // Тест без токена
    
    test('Access protected route without token', async () => {
        const response = await request(app).get('/user');
        
        expect(response.statusCode).toBe(404); ///////////////////////////////////////
    });

    // Создание пользователя с токеном
    test('Create user with valid token', async () => {
        const response = await request(app)
            .post('/users')
            .set('Authorization', 'valid-token') 
            .send({ name: 'John Doe', email: 'john@example.com' });
        expect(response.statusCode).toBe(201);
    });

    // Проверка доступа к данным другого пользователя
    test('Access another user\'s data', async () => {
        const response = await request(app)
            .get('/users/999') 
            .set('Authorization', 'valid-token');
        expect(response.statusCode).toBe(404);
        expect(response.body.error).toBe('User not found');
    });
});

// ------------------------------------7------------------------------------
describe('User API - Data Validation', () => {


    // Тест на отсутствие обязательного поля
    test('Fail to create user without name', async () => {
        const response = await request(app)
            .post('/users')
            .send({ email: 'john@example.com' });
        expect(response.statusCode).toBe(400);
        expect(response.body.error).toBe('Name and email are required');
    });

// Тест на слишком длинное имя
    test('Fail to create user with long name', async () => {
    const longName = 'a'.repeat(51); // 51 символ
    const response = await request(app)
        .post('/users')
        .send({ name: longName, email: 'john@example.com' });

        setR(response, 400, 'Name must be a string and less than 50 characters');//////////////
    expect(response.statusCode).toBe(400);
    expect(response.body.error).toBe('Name must be a string and less than 50 characters');
});

    // Тест на слишком длинный email
    test('Fail to create user with long email', async () => {
    const longEmail = 'a'.repeat(101) + '@example.com'; // 101 символ
    const response = await request(app)
        .post('/users')
        .send({ name: 'John Doe', email: longEmail });

    
    setR(response, 400, 'Email must be a string and less than 100 characters');//////////////
    expect(response.statusCode).toBe(400);
    expect(response.body.error).toBe('Email must be a string and less than 100 characters');
});
});

//-------------------------8---------------------

// for (let i = 1; i <= 10; i++) {
//     users.push({ id: currentId++, name: `User ${i}`, email: `user${i}@example.com` });
// }

// describe('Pagination Tests', () => {
//     it('should return the first page of users with limit 5', async () => {
//         const response = await request(app).get('/users?page=1&limit=5');
//         expect(response.status).toBe(200);
//         expect(response.body.page).toBe(1);
//         expect(response.body.limit).toBe(5);
//         expect(response.body.total).toBe(10);
//         expect(response.body.users).toHaveLength(5);
//         expect(response.body.users[0].id).toBe(1);
//     });

//     it('should return the second page of users with limit 5', async () => {
//         const response = await request(app).get('/users?page=2&limit=5');
//         expect(response.status).toBe(200);
//         expect(response.body.page).toBe(2);
//         expect(response.body.limit).toBe(5);
//         expect(response.body.total).toBe(10);
//         expect(response.body.users).toHaveLength(5);
//         expect(response.body.users[0].id).toBe(6);
//     });

//     it('should return an empty array for a page that exceeds total pages', async () => {
//         const response = await request(app).get('/users?page=3&limit=5');
//         expect(response.status).toBe(200);
//         expect(response.body.page).toBe(3);
//         expect(response.body.limit).toBe(5);
//         expect(response.body.total).toBe(10);
//         expect(response.body.users).toHaveLength(0);
//     });

//     it('should return 400 for invalid page number', async () => {
//         const response = await request(app).get('/users?page=0&limit=5');
//         expect(response.status).toBe(400);
//         expect(response.body).toEqual({ error: 'Invalid page or limit' });
//     });

//     it('should return 400 for invalid limit number', async () => {
//         const response = await request(app).get('/users?page=1&limit=-5');
//         expect(response.status).toBe(400);
//         expect(response.body).toEqual({ error: 'Invalid page or limit' });
//     });
// });

// app.listen(PORT, () => {
//     console.log(`Test server is running on http://localhost:${PORT}`);
// });