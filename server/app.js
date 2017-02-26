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
    client.get('followers/list.json', {
      //  screen_name: 'michaelhazani'
        screen_name: user
    }, function(error, tweet, response) {
        if (error) console.log(error);
        //res.send(response); // Tweet body.
        var response_array = JSON.parse(response["body"])["users"];
        console.log(typeof response_array); // Raw response object.
        res.send(response_array);

        // for (i in response) {
        //   console.log(response[i].screen_name);
        // }
        // res.send(usernames);
        // console.log(usernames);
    });
});

app.listen(3000, function () {
  console.log('Example app listening on port 3000!');
});

app.get('/test', function(req, res) {
    client.get('search/tweets', {
        q: 'node.js'
    }, function(error, tweets, response) {
        console.log(tweets);
    });
});
