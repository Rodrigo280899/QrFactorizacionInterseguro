/**
 * Middleware para validar JWT en rutas protegidas.
 * 
 * Decisiones técnicas:
 * - Se utiliza jsonwebtoken para verificar la autenticidad y validez del token.
 * - El secreto y el issuer se obtienen de variables de entorno para mayor seguridad y flexibilidad.
 * - Si el token es válido, el payload se adjunta a req.user para su uso posterior.
 * - Si el token es inválido o no está presente, se responde con el código de error correspondiente.
 */

const jwt = require('jsonwebtoken');

// Se recomienda NO dejar valores por defecto sensibles en producción.
const jwtSecret = process.env.JWT_SECRET || 'R0DR1G0_R0J4$_LUY0_PRUEBA_INTERSEGUROS@!';
const jwtIssuer = process.env.JWT_ISSUER || 'qr-factorizacion-api';

/**
 * Valida el JWT presente en el header Authorization.
 * Si es válido, permite el acceso a la ruta protegida.
 * Si no, responde con 401 o 403 según corresponda.
 */
function authenticateToken(req, res, next) {
    const authHeader = req.headers['authorization'];
    // El formato esperado es: "Bearer <token>"
    const token = authHeader && authHeader.split(' ')[1];

    if (!token) {
        return res.status(401).json({ error: 'Token requerido.' });
    }

    jwt.verify(token, jwtSecret, { issuer: jwtIssuer }, (err, user) => {
        if (err) {
            return res.status(403).json({ error: 'Token inválido o expirado.' });
        }
        req.user = user; // El payload del JWT queda disponible para el resto de la app
        next();
    });
}

module.exports = authenticateToken;