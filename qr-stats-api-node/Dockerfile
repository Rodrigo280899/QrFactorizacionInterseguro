FROM node:20

# Establecer el directorio de trabajo dentro del contenedor
WORKDIR /app

# Copiar los archivos de dependencias
COPY package*.json ./

# Instalar las dependencias
RUN npm install

# Copiar el resto de archivos al contenedor
COPY . .

# Exponer el puerto que usas en tu app
EXPOSE 3000

# Comando para arrancar la app desde src/app.js
CMD ["node", "src/app.js"]