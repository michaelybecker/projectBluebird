var express = require('express');
var app = express();
var Twitter = require('twitter');
var creds = require('./credsss.js');
var errMsg, sucMsg;
var error = function(err, response, body) {
    console.log('ERROR \n');
    console.log(err);
    errMsg = err;
};
var fs = require('fs');

var success = function(data) {
    console.log('Success!\n' + data);
    sucMsg = data;
}
//Get this data from your twitter apps dashboard
var client = new Twitter({
    consumer_key: creds.consumer_key,
    consumer_secret: creds.consumer_secret,
    access_token_key: creds.access_token_key,
    access_token_secret: creds.access_token_secret
});


var offline = true;

// var params = {
//     screen_name: 'michaelhazani'
// };

// TODO parsed string of users
app.get('/', function(req, res) {
    res.send('Hello World!');
});

var usernames = {};

// function getScreenNames(user){
//   console.log(user["screen_name"])
//   return user["screen_name"];
// }

// function concatScreenNames (user_arr){
//   var concat;
//   user_arr.forEach(function(user){
//     concat += (user["screen_name"] + ",");
//   });
//   return concat;
// }

app.get('/followers/:twitterUser', function(req, res) {
  if (offline) {
    var path =  __dirname + "/dummyData/dummy.JSON";
    var localJSON;
    fs.readFile(path, "utf-8", function(err, data) {
      var form = data.replace(/\\/g, "");
      localJSON = JSON.parse(data);
      //var formattedStr = localJSON.replace(/\\/g, "");
      if(err) throw err

      //create data objects that'll be passed to twitter



      // array in object, or object?
      var followers = [];
      // followers.follower = [];

      var body = JSON.parse(localJSON["body"]);
      var users = (body["users"]);
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
        //array
        // followers.follower.push(tempfollower);

        //object
        followers.push(tempfollower);

      }

      var followersString = JSON.stringify(followers);
      res.send(followersString);

      //test locally
          console.log(followers);

    console.log("sent followers//local!");

    });
  } else {
    client.get('followers/list.json', {
        screen_name: req.params.twitterUser
    }, function(error, tweet, response) {
        if (error) console.log(error);
        var response_array = JSON.parse(response["body"])["users"];
        var responseStr = JSON.stringify(response);
        // var formattedStr = responseStr.replace(/\\/g, "");
        // response_array = object_array.map(getScreenNames);
      //  var response_array = concatScreenNames(JSON.parse(response["body"])["users"]);
        // console.log(typeof response_array); // Raw response object.
        var path = __dirname + "/dummyData/dummy.JSON";
        fs.writeFile(path, responseStr, function(err) {
          if(err) {
            return console.log(err);
          }
          console.log("file saved!");
        })
        res.send(response_array);
    });
}
});

app.get('/dummy/:num', function(req, res) {
// Raw response object.
var numberThis = req.params.num;
    var returnMe = "";
    for (var i =0; i < numberThis; i++) {
  returnMe+= i+",";
        }
  res.send(returnMe);
});

//API: /following/:username retrieves array of username's following

app.get('/following/:twitterUser', function (req, res){
  if (offline) {
    var path =  __dirname + "/dummyData/dummy.JSON";
    var localJSON;
    fs.readFile(path, "utf-8", function(err, data) {
      localJSON = JSON.parse(data);
      if(err) throw err
    res.send(JSON.stringify(localJSON));
    console.log(localJSON["body"])["users"];
    console.log("sent local!");
  });
} else {
  client.get('friends/list.json', {
    screen_name: req.params.twitterUser
  }, function(error, user, response){
    if (error) console.log(error);
    //var response_array = JSON.parse(response["body"])["users"];
  //  response_array = object_array.map(getScreenNames);
   var response_array = concatScreenNames(JSON.parse(response["body"])["users"]);
    console.log(typeof response_array); // Raw response object.
    res.send(response_array);
  })
  }
})

app.listen(3000, function () {
  console.log('Example app listening on port 3000!');
  if(offline) {
    console.log('Running in Offline test mode!');
  } else {
    console.log('WARNING: running LIVE!');
  }
});

app.get('/test', function(req, res) {
    client.get('search/tweets', {
        q: 'node.js'
    }, function(error, tweets, response) {
        console.log(tweets);
    });
});
