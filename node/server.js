net = require('net')


var s = net.Server(function (socket) {


  socket.on('connect', function (socket) {
    console.log("demonbuddy connected...")
  })

  socket.on('data', function (msg_sent) {

    var data = JSON.parse(msg_sent)

    if (data.hasOwnProperty("msg")){
      console.log(data.msg + " / gph: " + data.gph)
    } else{
      console.log(msg_sent.toString("binary"))
    }

  })

  socket.on('end', function (socket) {
    console.log('connection closed')
  })
})

s.listen(9050)
console.log('System waiting at http://localhost:9050')