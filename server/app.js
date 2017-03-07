// testing / real query flag
var offline = true;

if (process.argv[2] == "online") {
    offline = false;
}

var express = require('express');
var app = express();
var fs = require('fs');
var Twitter = require('twitter');
var creds = require('./credsss.js');
var errMsg, sucMsg;
var error = function(err, response, body) {
    console.log('ERROR \n');
    console.log(err);
    errMsg = err;
};

var success = function(data) {
    console.log('Success!\n' + data);
    sucMsg = data;
}
// app.get('/', function(req, res) {
//     res.send('Hello World!');
// });


//OAuth data
var client = new Twitter({
    consumer_key: creds.consumer_key,
    consumer_secret: creds.consumer_secret,
    access_token_key: creds.access_token_key,
    access_token_secret: creds.access_token_secret
});

app.listen(3000, function() {
    console.log('Example app listening on port 3000!');
    if (offline) {
        console.log('Running in Offline test mode!');
        getFollowers('michaelhazani');
    } else {
        console.log('WARNING: running LIVE!');
        getFollowers('michaelhazani');
    }
});

function getFollowers(username) {
    var localJSON, followersString;
    if (offline) {
        var path = __dirname + "/dummyData/dummy.JSON";
        fs.readFile(path, "utf-8", function(err, data) {
            if (err) throw err;
            // console.log(data);
            var parsedData = JSON.parse(data);
            var followers = CreateFollowersObject(parsedData);
            var followersString = JSON.stringify(followers);
            console.log("sending followers//local!");
            console.log(followers);
            return followersString;
        });
    } else {
        client.get('followers/list.json', {
            screen_name: username
        }, function(error, tweet, response) {
            console.log("live: ");
            // console.log(response);
            if (error) console.log(error);
            //localJSON = response;
            var followers = CreateFollowersObject(response);
            var followersString = JSON.stringify(followers);
            console.log("sending followers//LIVE FROM API");
            console.log(followers);
            return followersString;
        });
    }
}

app.get('/followers/:twitterUser', function(req, res) {
res.send(getFollowers(req.params.twitterUser));
// for dummy local JSON file creation - do not touch otherwise
// var path = __dirname + "/dummyData/dummy.JSON";
// fs.writeFile(path, responseStr, function(err) {
//   if(err) {
//     return console.log(err);
//   }
//   console.log("file saved!");
// })
});

//API: /following/:username retrieves array of username's following
// app.get('/following/:twitterUser', function (req, res){
//   if (offline) {
//     var path =  __dirname + "/dummyData/dummy.JSON";
//     var localJSON;
//     fs.readFile(path, "utf-8", function(err, data) {
//       localJSON = JSON.parse(data);
//       if(err) throw err
//     res.send(JSON.stringify(localJSON));
//     console.log(localJSON["body"])["users"];
//     console.log("sent local!");
//   });
// } else {
//   client.get('friends/list.json', {
//     screen_name: req.params.twitterUser
//   }, function(error, user, response){
//     if (error) console.log(error);
//     //var response_array = JSON.parse(response["body"])["users"];
//   //  response_array = object_array.map(getScreenNames);
//    var response_array = concatScreenNames(JSON.parse(response["body"])["users"]);
//     console.log(typeof response_array); // Raw response object.
//     res.send(response_array);
//   })
//   }
// })

//-----------------------------------DATA PARSING HELPERS---------------------------------------------
//create data objects that'll be passed to twitter
function CreateFollowersObject(data) {
    var followers = [];
    var body = JSON.parse(data["body"]);
    var users = body["users"];
    for (obj in users) {
        var tempfollower = {};
        tempfollower["id"] = users[obj]["id"];
        tempfollower["screen_name"] = users[obj]["screen_name"];
        tempfollower["name"] = users[obj]["name"];
        tempfollower["location"] = users[obj]["location"];
        tempfollower["url"] = users[obj]["url"];
        tempfollower["description"] = users[obj]["description"];
        tempfollower["followers_count"] = users[obj]["followers_count"];
        tempfollower["friends_count"] = users[obj]["friends_count"];
        //object
        followers.push(tempfollower);
    }
    return followers;
}
