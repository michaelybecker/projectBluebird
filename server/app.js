var express = require('express');
var app = express();
var Twitter = require('twitter');
var creds = require('./creds.js');
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
//Get this data from your twitter apps dashboard
var client = new Twitter({
    consumer_key: creds.consumer_key,
    consumer_secret: creds.consumer_secret,
    access_token_key: creds.access_token_key,
    access_token_secret: creds.access_token_secret
});

var params = {
    screen_name: 'michaelhazani'
};


app.get('/', function(req, res) {
    res.send('Hello World!');
});

var usernames = {};

app.get('/followers/:twitterUser', function(req, res) {
    var user = req.params.twitterUser;
    var followers;
    //res.send(user);
    console.log(user);
    client.get('followers/list.json', {
        screen_name: req.params.twitterUser
    }, function(error, tweet, response) {
        if (error) {
          //console.log(error)
          var followers = "";
          for (var i = 0; i < 100; i++) {
            followers +=  i + "naawEGAWEdgd, ";
          }
          var followersToSend = JSON.stringify(followers);
          res.send(followersToSend);
        };

        //res.send(response); // Tweet body.
        var jsoned = JSON.parse(response["body"]);
        res.send(jsoned);
        console.log(jsoned); // Raw response object.

        // for (i in response) {
        //   console.log(response[i].screen_name);
        // }
        // res.send(usernames);
        // console.log(usernames);
    });
});

app.get('/dummyfollowers/:number', function(req, res) {
  var num = req.params.number;
  var followers = "";
  for (var i = 0; i < num; i++) {
    followers +=  i + "name, ";
  }

  var followersToSend = JSON.stringify(followers);
  res.send(followersToSend);
});

app.listen(3000, function () {
  console.log('app listening on port 3000!');
});
