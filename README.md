
# Sentiment Analysis API

This repository provides an API for performing sentiment analysis on text data using a machine learning model. The API is built using .NET and leverages ML.NET for sentiment analysis.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
  - [API Endpoints](#api-endpoints)
  - [Request and Response Examples](#request-and-response-examples)
- [Configuration](#configuration)
- [Running Tests](#running-tests)
- [Contributing](#contributing)
- [License](#license)

## Introduction

The Sentiment Analysis API allows developers to integrate sentiment analysis capabilities into their applications. It classifies text as positive or negative based on the sentiment detected.

## Features

- Analyze text sentiment using a pre-trained ML.NET model.
- Easy-to-use RESTful API.
- JSON requests and responses.

## Installation

Follow these steps to set up and run the Sentiment Analysis API:

1. **Clone the repository**:
    ```bash
    git clone https://github.com/PradeepLoganathan/SentimentAnalysisAPI.git
    cd SentimentAnalysisAPI
    ```

2. **Install the required .NET SDK**:
    Ensure you have the .NET SDK installed. You can download it from [here](https://dotnet.microsoft.com/download).

3. **Restore the dependencies**:
    ```bash
    dotnet restore
    ```

4. **Build the application**:
    ```bash
    dotnet build
    ```

5. **Run the application**:
    ```bash
    dotnet run
    ```

## Usage

### API Endpoints

#### Analyze Sentiment

- **Endpoint**: `/api/v1/sentiment`
- **Method**: `POST`
- **Description**: Analyzes the sentiment of the provided text.

### Request and Response Examples

#### Analyze Sentiment

- **Request**:
    ```http
    POST /api/v1/sentiment
    Content-Type: application/json

    {
        "text": "I love this product!"
    }
    ```

- **Response**:
    ```json
    {
        "sentiment": "positive"
    }
    ```

## Configuration

The application can be configured using the `appsettings.json` file or environment variables. Ensure that the model path is correctly set in the configuration file.

## Running Tests

To run the tests, use the following command:

```bash
dotnet test
```

Ensure you have added your test cases in the `tests` directory.

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Make your changes.
4. Submit a pull request.

Ensure your code follows the project's coding standards and includes appropriate tests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
