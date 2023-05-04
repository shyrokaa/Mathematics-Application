import requests
import json

url = "http://localhost:5000/genetic_algorithm"

data = {
    "generations": 10,
    "population_size": 100,
    "num_parents": 20,
    "num_vars": 1,
    "bottom": -5,
    "top": 5,
    "equation": "x0 * x0 - 4"
}

headers = {'Content-type': 'application/json'}

response = requests.post(url, data=json.dumps(data), headers=headers)

print(response.text)
