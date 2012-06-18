var app = require('http').createServer(handler)
  , io = require('socket.io').listen(app)
  , fs = require('fs')

net = require('net')
util = require('util')

var lastStats;

io.set('log level', 1)
app.listen(33333);



function handler (req, res) {
  fs.readFile(__dirname + '/index.html',
  function (err, data) {
    if (err) {
      res.writeHead(500);
      return res.end('Error loading index.html');
    }

    res.writeHead(200);
    res.end(data);
  });
}

io.sockets.on('connection', function (socket) {
  util.log('socketio connected')
  socket.emit('pulse', lastStats)
});


var port = 9050

var s = net.Server(function (socket) {


  socket.on('connect', function (socket) {
    util.log("demonbuddy connected...")
  })

  socket.on('data', function (msg_sent) {
    try{
      var data = JSON.parse(msg_sent)

      if (data.hasOwnProperty('event')){
        util.log('new event: ' + data.event)
        handleIncomingEvent(data)
      } else{
        util.log(msg_sent.toString("binary"))
      }
    }
    catch (e){
      util.log('JSON PARSE ERROR WITH ' + msg_sent.toString("binary") + "\n:\n" + e)
    }


  })

  socket.on('end', function (socket) {
    util.log('connection closed')
  })
})

s.listen(port)

util.log('System waiting at http://localhost:' + port)

function handleIncomingEvent (data) {
  if (data.event == 'left'){
    lastStats = data.stats
    io.sockets.emit('pulse', lastStats)
  }
  if (data.event == 'loot'){
    io.sockets.emit('loot', { msg: data.msg})
  }
}