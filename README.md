# Armut.CaseStudy
# windows_amd64.exe build -i Armut.postman_collection.json -o ./index.md -m

# Armut



## Indices

* [Message-Service](#message-service)

  * [List Messages](#1-list-messages)
  * [Send Message](#2-send-message)

* [User-Services](#user-services)

  * [Blocked User](#1-blocked-user)
  * [Login](#2-login)
  * [Singup](#3-singup)


--------


## Message-Service
Chat messages management services



### 1. List Messages



***Endpoint:***

```bash
Method: POST
Type: RAW
URL: https://localhost:5001/api/message/list-messages
```



***Body:***

```js        
{
    "username":"test1",
    "checkUser":"test2",
    "startTime": "",
    "endTime":""
}
```



### 2. Send Message


send message by recevier userId


***Endpoint:***

```bash
Method: POST
Type: RAW
URL: https://localhost:5001/api/message/send-message
```



***Body:***

```js        
{
    "senderUsername" : "test1",
    "receiveUsername" : "test2",
    "message" : "hello world22"
}
```



## User-Services
User information management and check user info 



### 1. Blocked User



***Endpoint:***

```bash
Method: POST
Type: RAW
URL: https://localhost:5001/api/user/block-user
```



***Body:***

```js        
{
    "Username":"test2",
    "BlockedUser":"test3"
}
```



### 2. Login



***Endpoint:***

```bash
Method: POST
Type: RAW
URL: https://localhost:5001/api/user/login
```



***Body:***

```js        
{
    "Username":"test1",
    "Password":"12345"
}
```



### 3. Singup


Singup with username, password, and name


***Endpoint:***

```bash
Method: POST
Type: RAW
URL: https://localhost:5001/api/user/signup
```



***Body:***

```js        
{
    "username":"test2",
    "password":"12345"
}
```



***Available Variables:***

| Key | Value | Type |
| --- | ------|-------------|
| host | https://localhost:{{port}}/api |  |
| port | 5001 |  |



---
[Back to top](#armut)

