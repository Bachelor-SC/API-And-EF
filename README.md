# API-And-EF
# API Documentation
## Activity
| Type | Path | Parameter | Body | Returns |
|---|---|---|---|---|
| GET | /Activity/LatestActivity?user={STRING} | username | N/A | {  "type": "string",  "action": "string",  "date": "DateTimeObject",  "additionalInfo": "string",  "username":"string"} |
| GET | /Activity/AllActivities?user={STRING} |username | N/A | {  "type": "string",  "action": "string",  "date": "DateTimeObject",  "additionalInfo": "string",  "username":"string"} |
| POST | /Activity/LogActivity?user={STRING}| username | {  "type": "string",  "action": "string",  "date": "DateTimeObject",  "additionalInfo": "string",  "username":"string"} |  {  "type": "string",  "action": "string",  "date": "DateTimeObject",  "additionalInfo": "string",  "username":"string"} |

## Coaches
| Type | Path | Parameter | Body | Returns |
|---|---|---|---|---|
| GET | /Coaches/Coach?coachToBeFound={STRING} | coachToBeFound | N/A | {  "username": "string",  "name": "string",  "shortDesc": "string",  "content": "string",  "picture": "string"} |
| GET | /Coaches/AllCoaches | N/A | N/A | {  "username": "string",  "name": "string",  "shortDesc": "string",  "content": "string",  "picture": "string"} |
| POST |/Coaches/registerCoach| N/A | {  "type": "string",  "action": "string",  "date": "DateTimeObject",  "additionalInfo": "string",  "username":"string"} |  {  "username": "string",  "name": "string",  "shortDesc": "string",  "content": "string",  "picture": "string"} |

## Connections
| Type | Path | Parameter | Body | Returns |
|---|---|---|---|---|
| GET | /Connections/AllConnections?user={STRING} | N/A | N/A | [  {    "usernameCon1": "string",    "usernameCon2": "string",    "createdDate": "DateTimeObject"
  }
] |
| POST | /Connections/AddConnection | N/A | {  "usernameCon1": "string",  "usernameCon2": "string",  "createdDate": "DateTimeObject"} | {  "usernameCon1": "string",  "usernameCon2": "string",  "createdDate": "DateTimeObject"} |
| GET | /Connections/CheckIfAlreadyConnected?user={STRING}&connectionUsername={STRING} | username & username | N/A | BOOLEAN |
| DELETE | /Connections/DeleteConnection?user={STRING}&connectionUsername={STRING} | username & username | N/A | BOOLEAN |
| GET | /Connections/BlocksForUser?user={STRING} | username | N/A | {  "usernameCon1": "string",  "usernameCon2": "string",  "createdDate": "DateTimeObject"} |
| POST | /Connections/AddBlock | N/A | {  "usernameCon1": "string",  "usernameCon2": "string",  "createdDate": "DateTimeObject"} | {  "usernameCon1": "string",  "usernameCon2": "string",  "createdDate": "DateTimeObject"} |
| GET | /Connections/CheckIfBlocked?user={STRING}&connectionUsername={STRING} | username & username | N/A | string |
| DELETE | /Connections/DeleteBlock?user={STRING}&connectionUsername={STRING} | username & username | N/A | BOOLEAN |

## Timeline
| Type | Path | Parameter | Body | Returns |
|---|---|---|---|---|
| GET | /Timeline/GetAllComments | N/A | N/A | [  {    "commentID": 0,    "username": "string",    "comment": "string",    "likes": 0,    "postID": 0,    "createdDate": "DateTimeObject"  }] |
| POST | /Timeline/AddComment | N/A |  {    "commentID": 0,    "username": "string",    "comment": "string",    "likes": 0,    "postID": 0,    "createdDate": "DateTimeObject"  } |  {    "commentID": 0,    "username": "string",    "comment": "string",    "likes": 0,    "postID": 0,    "createdDate": "DateTimeObject"  } |
| POST | /Timeline/AddPost | N/A | {  "postID": 0,  "username": "string",  "content": "string",  "likes": 0,  "createdDate": "DateTimeObject"} | {  "postID": 0,  "username": "string",  "content": "string",  "likes": 0,  "createdDate": "DateTimeObject"} |
| GET | /Timeline/GetAllPosts?username={STRING} | username | N/A | [  {    "postID": 0,    "username": "string",    "content": "string",    "likes": 0,    "createdDate": "DatetimeObject"  }] |
| POST | /Timeline/LikePost | N/A | "string" | N/A |
| POST | /Timeline/LikeComment | N/A | "string" | N/A |

## User
| Type | Path | Parameter | Body | Returns |
|---|---|---|---|---|
| GET | /User/Login?username={STRING}&password={STRING} | username & password | N/A | {  "username": "string",  "hashedPassword": "string",  "subscriptionLevel": 0} |
| POST | /User/Signup | N/A | {  "username": "string",  "hashedPassword": "string",  "subscriptionLevel": 0,  "socialMedia": {    "username": "string",    "facebook": "string",    "linkedIn": "string",    "instagram": "string"  },  "description": "string",  "profilePicture": "string",  "coverPicture": "string",  "location": {    "username": "string",    "address": "string",    "postalCode": 0,    "city": "string"  },  "phonenumber": "string",  "email": "string",  "link": {    "username": "string",    "link": "string"  }} | {  "username": "string",  "hashedPassword": "string",  "subscriptionLevel": 0,  "socialMedia": {    "username": "string",    "facebook": "string",    "linkedIn": "string",    "instagram": "string"  },  "description": "string",  "profilePicture": "string",  "coverPicture": "string",  "location": {    "username": "string",    "address": "string",    "postalCode": 0,    "city": "string"  },  "phonenumber": "string",  "email": "string",  "link": {    "username": "string",    "link": "string"  }} |
| GET | /User/Info?username={STRING} | N/A | N/A | {  "username": "string",  "hashedPassword": "string",  "subscriptionLevel": 0,  "socialMedia": {    "username": "string",    "facebook": "string",    "linkedIn": "string",    "instagram": "string"  },  "description": "string",  "profilePicture": "string",  "coverPicture": "string",  "location": {    "username": "string",    "address": "string",    "postalCode": 0,    "city": "string"  },  "phonenumber": "string",  "email": "string",  "link": {    "username": "string",    "link": "string"  }} |
| PATCH | /User/Patch | N/A | N/A | {  "username": "string",  "hashedPassword": "string",  "subscriptionLevel": 0,  "socialMedia": {    "username": "string",    "facebook": "string",    "linkedIn": "string",    "instagram": "string"  },  "description": "string",  "profilePicture": "string",  "coverPicture": "string",  "location": {    "username": "string",    "address": "string",    "postalCode": 0,    "city": "string"  },  "phonenumber": "string",  "email": "string",  "link": {    "username": "string",    "link": "string"  }} |
| DELETE | /User/{username} | username | N/A | N/A |
