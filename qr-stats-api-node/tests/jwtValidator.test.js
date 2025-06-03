const request = require('supertest');
const express = require('express');
const matrixRoutes = require('../src/routes/matrix');

const app = express();
app.use(express.json());
app.use('/api/matrix', matrixRoutes);

describe('POST /api/matrix/calculate', () => {
    it('debe rechazar sin JWT', async () => {
        const res = await request(app)
            .post('/api/matrix/calculate')
            .send({ Q: [[1]], R: [[2]] });
        expect(res.statusCode).toBe(401);
    });
    it('debe rechazar con JWT inválido', async () => {
        const res = await request(app)
            .post('/api/matrix/calculate')
            .set('Authorization', 'Bearer token_invalido')
            .send({ Q: [[1]], R: [[2]] });
        expect(res.statusCode).toBe(403);
        expect(res.body.error).toMatch(/Token inválido/i);
    });
    it('debe rechazar si el header Authorization tiene formato incorrecto', async () => {
        const res = await request(app)
            .post('/api/matrix/calculate')
            .set('Authorization', 'token_sin_bearer')
            .send({ Q: [[1]], R: [[2]] });
        expect(res.statusCode).toBe(401);
        expect(res.body.error).toMatch(/Token requerido/i);
    });
});