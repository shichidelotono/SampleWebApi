# SampleWebApi

Examples (e.g. running Web App on Kestrel)

Insert
* URL - https://localhost:5001/api/cars
* Method - Post
* Body - {
	"colour" : "silver",
	"countrymanufactured" : "Japan",
	"make": "Subaru",
	"model" : "Outback",
	"price" : 14000,
	"year" : "2009"
}
* Sample response - {
    "isSuccess": true,
    "errorMessage": ""
}

Update
* URL - https://localhost:5001/api/cars/10
* Method - Put
* Body - {
	"colour" : "silver",
	"countrymanufactured" : "Japan",
	"make": "Subaru",
	"model" : "Outback",
	"price" : 14000,
	"year" : "2009"
}
* Sample response - {
    "isSuccess": true,
    "errorMessage": ""
}

Calculate Price
* URL - https://localhost:5001/api/price
* Method - Post
* Body - {
	"ids": [1, 2, 126]
}
* Sample response - {
    "price": 133617.5,
    "message": null,
    "isSuccess": true,
    "errorMessage": null
}
