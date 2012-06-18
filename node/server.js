net = require('net')
util = require('util')

var port = 9050

var s = net.Server(function (socket) {


  socket.on('connect', function (socket) {
    util.log("demonbuddy connected...")
  })

  socket.on('data', function (msg_sent) {

    var data = JSON.parse(msg_sent)

    if (data.hasOwnProperty("msg")){
      util.log(data.msg + " / gph: " + data.gph)
    } else{
      util.log(msg_sent.toString("binary"))
    }

  })

  socket.on('end', function (socket) {
    util.log('connection closed')
  })
})

s.listen(port)

util.log('System waiting at http://localhost:' + port)