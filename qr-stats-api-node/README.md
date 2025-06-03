# Matrix API

This project implements a RESTful API for performing operations on matrices. It allows users to send two matrices, Q and R, and receive statistical calculations and checks on these matrices.

## Features

- Receive matrices Q and R via a POST request.
- Calculate statistics:
  - Maximum value
  - Minimum value
  - Average value
  - Total sum
- Check if either matrix is diagonal.

## Getting Started

### Prerequisites

- Node.js (version 14 or higher)
- npm (Node package manager)

### Installation

1. Clone the repository:
   ```
   git clone <repository-url>
   cd matrix-api
   ```

2. Install the dependencies:
   ```
   npm install
   ```

### Running the Application

To start the server, run:
```
npm start
```

The API will be available at `http://localhost:3000`.

### API Endpoints

- **POST /api/matrix**
  - Request Body:
    ```json
    {
      "Q": [[1, 0, 0], [0, 2, 0], [0, 0, 3]],
      "R": [[4, 0], [0, 5]]
    }
    ```
  - Response:
    ```json
    {
      "statistics": {
        "Q": {
          "max": 3,
          "min": 1,
          "average": 2,
          "totalSum": 6
        },
        "R": {
          "max": 5,
          "min": 4,
          "average": 4.5,
          "totalSum": 9
        }
      },
      "diagonal": {
        "Q": true,
        "R": true
      }
    }
    ```

### License

This project is licensed under the MIT License.