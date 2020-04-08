# Message Format
---

B = Byte
Control(1B) or Status(1B) or Count(4B) + [Size(4B) + Data(Size)] * N

---

Each "[Size(4B) + Data(Size)]" is a datablock, i.e. the "username", "password".... below are all of this format.

Client always send a control code of 1 byte, and additional data if applicable.
Server always return a status code of 1 byte or count of following datablock of 4 byte, and additional data if applicable.

## Client

### Login: 
Login with username and password.

Format: 0(1B) + username + password

Server returns 1(1B) if success, 0(1B) if no such account.

### Register:
Register with username and password.

Format: 1(1B) + username + password

Server returns 1(1B) if success, 0(1B) if error.
Close connection afterwards.

### Query History
Get chat history with username. Only valid after login.

Format: 2(1B)

Server returns 'number of records'(4B) + (username size + username + data size + data) * N

For file transmission records, simply use its filename as chat content.

### Chat
Send message to username. Only valid after login.

Format: 3(1B) + username + chat content

Server returns 1(1B) if success, 0(1B) if error.

### File Transfer
Send file to username. Only valid after login.

Format: 4(1B) + username + filename + file

Server returns 1(1B) if success, 0(1B) if error.

### Logout
Logout with current account.

Format: 5(1B)

Server returns 1(1B) if success, 0(1B) if error.

### Peek text
Ask server to send all text messages since last peek.

Format: 13(1B)

Server returns 'number of records'(4B) + (username size + username + data size + data) * N

### Peek file
Ask server to send all file messages since last peek.

Format: 14

Server returns 'number of records'(4B) + (username size + username + filenamesize + filename + data size + data) * N
