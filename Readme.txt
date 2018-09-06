EndPoints

	Persons
		GET ALL: http://localhost:60388/api/Persons

		GET BY ID: http://localhost:60388/api/Persons/1

		DELETE: http://localhost:60388/api/Persons/1

		PUT: http://localhost:60388/api/Persons/1
			[Body]
			{
				"Name": "Sebas",
				"Birthdate": "1990-11-02T00:00:00"
			}

		PATCH: http://localhost:60388/api/Persons/1
			[Body]
			[
				{
				  "op": "remove",
				  "path": "/Birthdate"
				},
				{
				  "op": "replace",
				  "path": "/name",
				  "value": "Pepe"
				}
			]
	
		POST: http://localhost:60388/api/Persons
			[Body]
			{
				"Name": "Sebas",
				"Birthdate": "1990-11-02T00:00:00"
			}

	
	Addresses
		GET ALL: http://localhost:60388/api/Persons/1/addresses

		GET BY ID: http://localhost:60388/api/Persons/1/addresses/2

		DELETE: http://localhost:60388/api/Persons/1/addresses/2

		PUT: http://localhost:60388/api/Persons/1/addresses/2
			[Body]
			{
				"Street": "Felip II",
				"Number": "523",
				"Province": "Bcn"
			}

		PATCH: http://localhost:60388/api/Persons/1/addresses/2
			[Body]
			[
				{
				  "op": "remove",
				  "path": "/Province"
				},
				{
				  "op": "replace",
				  "path": "/Street",
				  "value": "Canale 2851"
				}
			]
	
		POST: http://localhost:60388/api/Persons/1/addresses
			[Body]
			{
				"Street": "Felip II",
				"Number": "523",
				"Province": "Bcn"
			}

