# SimpleChat
# Simple Client/Server Chat Application  

This project is a basic client/server chat application built in **C#**. It uses the **Socket abstraction** (`TcpClient`/`TcpListener`) for communication and **threads** for concurrent processing. The application allows users to launch either the server or the client directly at startup by specifying their role.  

## Project Overview  

The application enables multiple clients to connect to a central chat server for real-time message exchange. The server manages connections and broadcasts messages to all connected clients, while clients can send and receive messages seamlessly.  

### Key Features  

1. **Launch by Role**:  
   - Users can launch the application as a server or client by typing `server` or `client` at startup.  

2. **Socket Abstraction**:  
   - The server utilizes `TcpListener` to accept client connections.  
   - Clients use `TcpClient` to establish communication with the server.  

3. **Multithreading**:  
   - The server creates a thread for each client, enabling concurrent handling of multiple connections.  

4. **Real-Time Messaging**:  
   - Clients send messages to the server, which are broadcast to all connected clients.  

5. **Error Handling and Resource Cleanup**:  
   - Robust handling of dropped connections and resource management for smooth operation.  

---

## Skills Gained  

This project helped enhance my understanding and skills in the following areas:  

1. **Network Programming**:  
   - Mastered the use of `TcpClient` and `TcpListener` for creating socket-based communication systems.  

2. **Multithreading**:  
   - Learned to use threads for handling simultaneous connections efficiently.  

3. **Client/Server Design**:  
   - Built a simple yet robust architecture for chat applications, focusing on scalability and usability.  

4. **Command-Line Interaction**:  
   - Added functionality to specify application roles (server or client) at startup for better user experience.  

5. **Error Handling and Debugging**:  
   - Implemented safeguards to handle dropped connections and errors gracefully.  

---

This project demonstrates a straightforward yet functional implementation of a chat system, combining networking and concurrency concepts. 
