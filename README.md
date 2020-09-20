# Armut.CaseStudy
 

# Armut



## Indices

* [Ungrouped](#ungrouped)

  * [Login](#1-login)
  * [Send Message](#2-send-message)
  * [Singup](#3-singup)


--------


## Ungrouped



### 1. Login



***Endpoint:***

```bash
Method: POST
Type: RAW
URL: https://localhost:44373/api/user
```



***Body:***

```js        
{
    "Username" : "hakan",
    "Password"  : "1234"
}
```



### 2. Send Message


send message by recevier userId


***Endpoint:***

```bash
Method: POST
Type: RAW
URL: https://localhost:5001/api/user/message/send-message
```



***Body:***

```js        
{
    "username" : "hakan",
    "message" : "hello"
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
    "username":"hakan5",
    "password":"12345"
}
```



---
[Back to top](#armut)
> Made with &#9829; by [thedevsaddam](https://github.com/thedevsaddam) | Generated at: 2020-09-20 13:45:01 by [docgen](https://github.com/thedevsaddam/docgen)
