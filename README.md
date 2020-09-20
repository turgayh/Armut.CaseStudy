# Armut
## Installation

```bash
git clone https://github.com/turgayh/Armut.CaseStudy.git
cd Armut.CaseStudy
docker-compose up -d
```

## Indices

* [Health Check](#health-check)

  * [Check App Running With JWT token](#1-check-app-running-with-jwt-token)
  * [Check App Runnning](#2-check-app-runnning)

* [Message-Service](#message-service)

  * [List Messages](#1-list-messages)
  * [Send Message](#2-send-message)

* [User-Services](#user-services)

  * [Blocked User](#1-blocked-user)
  * [Login](#2-login)
  * [Singup](#3-singup)


--------


## Health Check



### 1. Check App Running With JWT token



***Endpoint:***

```bash
Method: GET
Type: 
URL: http://localhost:5000/api/HealthCheck/auth
```



### 2. Check App Runnning



***Endpoint:***

```bash
Method: GET
Type: 
URL: http://localhost:5000/api/HealthCheck
```



## Message-Service
Chat messages management services



### 1. List Messages



***Endpoint:***

```bash
Method: POST
Type: RAW
URL: http://localhost:5000/api/message/list-messages
```



***Body:***

```js        
{
    "username":"test",
    "checkUser":"test1"
}
```



### 2. Send Message


send message by recevier userId


***Endpoint:***

```bash
Method: POST
Type: RAW
URL: http://localhost:5000/api/message/send-message
```



***Body:***

```js        
{
    "senderUsername" : "test",
    "receiveUsername" : "test1",
    "message" : "backend test"
}
```



## User-Services
User information management and check user info 



### 1. Blocked User



***Endpoint:***

```bash
Method: POST
Type: RAW
URL: http://localhost:5000/api/user/block-user
```



***Body:***

```js        
{
    "Username":"test",
    "BlockedUser":"test2"
}
```



### 2. Login



***Endpoint:***

```bash
Method: POST
Type: RAW
URL: http://localhost:5000/api/user/login
```



***Body:***

```js        
{
    "Username":"test",
    "Password":"12345"
}
```



### 3. Singup


Singup with username, password, and name


***Endpoint:***

```bash
Method: POST
Type: RAW
URL: http://localhost:5000/api/user/signup
```



***Body:***

```js        
{
    "username":"test",
    "password":"12345"
}
```



***Available Variables:***

| Key | Value | Type |
| --- | ------|-------------|
| host-http | http://localhost:{{port}}/api |  |
| port | 5000 |  |
| host-https | https://localhost:5001/api |  |



---
[Back to top](#armut)

