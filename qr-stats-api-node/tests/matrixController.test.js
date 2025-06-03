const MatrixController = require('../src/controllers/matrixController');

// Mock de dependencias
jest.mock('../src/validators/matrixValidator', () => ({
    validate: jest.fn()
}));
jest.mock('../src/services/matrixService', () => ({
    getMatrixInfo: jest.fn(() => ({ mock: true }))
}));

const MatrixValidator = require('../src/Validators/matrixValidator');
const MatrixService = require('../src/Services/matrixService');

describe('MatrixController', () => {
    let req, res, controller;

    beforeEach(() => {
        controller = new MatrixController();
        req = { body: { Q: [[1]], R: [[2]] } };
        res = {
            status: jest.fn().mockReturnThis(),
            json: jest.fn()
        };
        MatrixValidator.validate.mockReset();
        MatrixService.getMatrixInfo.mockClear();
    });

    it('debe responder 400 si la validación falla', () => {
        MatrixValidator.validate.mockReturnValue('Error de validación');
        controller.calculateStatistics(req, res);
        expect(res.status).toHaveBeenCalledWith(400);
        expect(res.json).toHaveBeenCalledWith({ error: 'Error de validación' });
    });
    it('debe llamar getMatrixInfo con las matrices correctas', () => {
        MatrixValidator.validate.mockReturnValue(null);
        controller.calculateStatistics(req, res);
        expect(MatrixService.getMatrixInfo).toHaveBeenCalledWith(req.body.Q);
        expect(MatrixService.getMatrixInfo).toHaveBeenCalledWith(req.body.R);
    });
    it('debe responder 400 si falta Q o R en el body', () => {
        req.body = { Q: [[1]] }; // Falta R
        MatrixValidator.validate.mockReturnValue('Las claves Q y R deben estar presentes en el cuerpo de la solicitud.');
        controller.calculateStatistics(req, res);
        expect(res.status).toHaveBeenCalledWith(400);
        expect(res.json).toHaveBeenCalledWith({ error: 'Las claves Q y R deben estar presentes en el cuerpo de la solicitud.' });
    });
    it('no debe llamar getMatrixInfo si la validación falla', () => {
        MatrixValidator.validate.mockReturnValue('Error');
        controller.calculateStatistics(req, res);
        expect(MatrixService.getMatrixInfo).not.toHaveBeenCalled();
    });

    it('debe responder 200 y retornar info de matrices si la validación pasa', () => {
        MatrixValidator.validate.mockReturnValue(null);
        controller.calculateStatistics(req, res);
        expect(MatrixService.getMatrixInfo).toHaveBeenCalledTimes(2);
        expect(res.json).toHaveBeenCalledWith({
            Q: { mock: true },
            R: { mock: true }
        });
    });
});