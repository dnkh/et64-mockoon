
## Install newman
```
npm install -g newman
```

## Install bruno/cli
```
npm install -g @usebruno/cli
```

## Run postman collection with newman

```
newman run .\weatherapi.postman_collection.json
```

## Run Bruno collection with bruno cli
```
bru run --env-var baseurl=http://localhost:5085
```
