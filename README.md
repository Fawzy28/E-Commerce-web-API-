# E-Commerce-web-API-
it's my first web API by asp.net core  - ntier architecture


##there some notes you should be aware of it before using the api :
1- if you register with any email you will get the user Role 
2- if your email contains the role name in it you will get this role 
3- in roles end points admin only can add a new roles then he can put it in the autorize attribute on any method 
4- admin can do the following :
* post new categories
* delete categories
* update categories
* get all oreders new orders or one order by id 
* update order status (shipped / new order)
* delete order
* post new products
* update product
* delete product
* get or post or update or delete roles

5- normal user can do the following :
* get the categories or get one
* post new order
* get all product or get one by id / category name
* can get the info of his account or update it or delete account


** don't forget to register then login and use the jwt token then make the categories and products and then you can make an order **
