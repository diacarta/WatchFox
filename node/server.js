net = require('net')


var s = net.Server(function (socket) {
  socket.on('data', function (msg_sent) {
    console.log(msg_sent.toString("binary"))
  })
  socket.on('end', function () {
    console.log('connection closed')
  })
})

s.listen(9050)
console.log('System waiting at http://localhost:9050')