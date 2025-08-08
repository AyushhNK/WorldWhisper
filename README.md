# WorldWhisper - Real-Time Chat Application

A modern real-time chat application built with .NET WebAPI and SignalR, featuring instant messaging capabilities with a clean and responsive user interface.

## 🚀 Features

- **Real-time messaging** using SignalR
- **User authentication** with JWT tokens
- **Role-based access control**
- **Instant message delivery**
- **RESTful API endpoints**

## 🛠️ Technology Stack

- **Backend**: .NET 8 WebAPI
- **Real-time Communication**: SignalR
- **Authentication**: ASP.NET Core Identity with JWT
- **Database**: Entity Framework Core
- **Frontend**: React with SignalR client
- **CORS**: Cross-Origin Resource Sharing enabled

## 📋 Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code
- SQL Server or SQLite
- Node.js (for frontend development)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd worldwhisper
```

### 2. Backend Setup

1. **Navigate to the API project:**
   ```bash
   cd WorldWhisper.API
   ```

2. **Install dependencies:**
   ```bash
   dotnet restore
   ```

3. **Configure the database connection** in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=WorldWhisperDB;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

4. **Run database migrations:**
   ```bash
   dotnet ef database update
   ```

5. **Start the API server:**
   ```bash
   dotnet run
   ```

   The API will be available at `https://localhost:7251`

### 3. Frontend Setup

1. **Navigate to the React app:**
   ```bash
   cd ../my-react-app
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Install SignalR client:**
   ```bash
   npm install @microsoft/signalr
   ```

4. **Start the development server:**
   ```bash
   npm run dev
   ```

   The React app will be available at `http://localhost:5173`

## 📡 API Endpoints

### Authentication

- **POST** `/login` - User login
  ```json
  {
    "username": "string",
    "password": "string"
  }
  ```

- **POST** `/register` - User registration
  ```json
  {
    "username": "string",
    "email": "string",
    "password": "string"
  }
  ```

### SignalR Hub

- **Hub URL**: `https://localhost:7251/chathub`
- **Methods**:
  - `SendMessage(user, message)` - Send a message
  - `ReceiveMessage(user, message)` - Receive a message

## 🔧 Configuration

### CORS Settings

The API is configured to allow requests from the React development server:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
```

### SignalR Configuration

SignalR is configured with automatic reconnection and authentication support:

```csharp
app.MapHub<ChatHub>("/chathub");
```

## 🎯 Usage

### 1. Start the Application

1. Start the .NET WebAPI backend
2. Start the React frontend
3. Open your browser to `http://localhost:5173`

### 2. Using the Chat

1. Enter your name in the "Your name" field
2. Type your message in the message input
3. Click "Send" to broadcast your message
4. Messages will appear in real-time for all connected users

### 3. API Testing

You can test the API endpoints using tools like Postman or curl:

```bash
# Login
curl -X POST https://localhost:7251/login \
  -H "Content-Type: application/json" \
  -d '{"username":"testuser","password":"password123"}'
```

## 🔒 Security Features

- JWT token-based authentication
- Password hashing with ASP.NET Core Identity
- CORS protection
- Input validation and sanitization

## 📁 Project Structure

```
worldwhisper/
├── WorldWhisper.API/          # .NET WebAPI backend
│   ├── Controllers/           # API controllers
│   ├── Hubs/                  # SignalR hubs
│   ├── Models/                # Data models
│   ├── Services/              # Business logic
│   └── appsettings.json       # Configuration
├── my-react-app/              # React frontend
│   ├── src/
│   │   ├── App.jsx           # Main chat component
│   │   └── main.jsx          # App entry point
│   └── package.json
└── README.md
```

## 🐛 Troubleshooting

### Common Issues

1. **CORS Errors**: Ensure the frontend URL is correctly configured in the CORS policy
2. **SignalR Connection Issues**: Check that the hub URL matches between frontend and backend
3. **Database Connection**: Verify the connection string in `appsettings.json`

### Debug Mode

To run in debug mode:

```bash
# Backend
dotnet run --environment Development

# Frontend
npm run dev
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

For support and questions:
- Create an issue in the repository
- Contact the development team
- Check the documentation

---

**Happy Chatting! 🎉**
