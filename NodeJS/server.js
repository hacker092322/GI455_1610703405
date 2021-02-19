var websocket = require('ws');

var websocketserver = new websocket.Server({port:25500}, ()=>{
    console.log("Chatroom server is running.");
});

var wsList = [];
var nameList = [];
var roomList = [];
var checkMessage;
        // 0 = Message from sender
        // 1 = Message from other
        // 2 = Connect Message
        // 3 = Disconnect Message
var checkSender;
var sendRoom;

websocketserver.on("connection",(ws,rq)=>{
    var firstTime = true;
    var yourWS = ws;
    var isConnect = false;

    ws.on("message", (data)=>{

        if (firstTime == true)
        {
            console.log("Login data: " + data);
            var loginDataSplit = data.split(';');
            // 0 = event
            // 1 = room name
            // 2 = user name
    
            if(loginDataSplit[0] == "Create")
            {
                console.log(loginDataSplit[2] + " request to create room.");
                var isSameNameRoom = false;
                for(var i = 0; i < roomList.length; i++)
                {
                   if(loginDataSplit[1] == roomList[i].roomName)
                   {
                     isSameNameRoom = true;
                     console.log("Same Room");
                     break;
                   }
                }
    
                if(isSameNameRoom == true)
                {
                  console.log(loginDataSplit[1] + " have created already.");
                  ws.send("haveSameRoomName");
                }
                else if (isSameNameRoom == false)
                {
                  console.log(loginDataSplit[1] + " have not created.");
                  var newRoom = {
                      roomName: loginDataSplit[1],
                      wsList: [],
                      nameList: []
                  }
                  newRoom.wsList.push(ws);
                  newRoom.nameList.push(loginDataSplit[2]);
                  roomList.push(newRoom);
                  console.log("Create room : " + loginDataSplit[1]);
                  ws.send("connect");
                  firstTime = false;
                }
            }
    
            else if (loginDataSplit[0] == "Join")
            {
                console.log(loginDataSplit[2] + " request to join room.");
                isSameNameRoom = false;
                for(var i = 0; i < roomList.length ;i++)
                {
                    if(loginDataSplit[1] == roomList[i].roomName)
                   {
                     isSameNameRoom = true;
                     console.log("Server have A request room.");
                     roomList[i].wsList.push(ws);
                     roomList[i].nameList.push(loginDataSplit[2]);
                     console.log(loginDataSplit[2] + " have join " + roomList[i].roomName);
                     ws.send("connect");
                     firstTime = false;
                     break;
                   }
                }
    
                if (isSameNameRoom == false)
                {
                    console.log(loginDataSplit[1] + " have not created yet.");
                    ws.send("don'tFountRoom");
                }
            }
        }
        
        else 
        {
            //Sender
            for (var i = 0; i < roomList.length; i++)
            {
              for (var j = 0; j < roomList[i].wsList.length; j++)
               {
                 if(roomList[i].wsList[j] == ws)
                {
                  sendRoom = i;
                  checkSender = j;
                  console.log("Send from "+ roomList[i].nameList[j] +" : " + data + " from room : " + roomList[i].roomName);
                  console.log("Sender Index : " + checkSender);
                  break;
                }
               }
            }

            //Boardcast
            for(var i = 0; i < roomList[sendRoom].wsList.length; i++)
         {
            if(i == checkSender)
            {
              checkMessage = 0;
              var sendYourMessage = checkMessage + ";" + data;
              roomList[sendRoom].wsList[i].send(sendYourMessage);
              continue;
            }
    
            else
            {
              checkMessage = 1;
              console.log("Send to " + roomList[sendRoom].nameList[i]);
              console.log("Send " + data);
              console.log("Send Index " + i);
              var sendToOther = checkMessage + ";" + roomList[sendRoom].nameList[checkSender] + ";" + data;
              roomList[sendRoom].wsList[i].send(sendToOther);
              console.log("Send Complete");
            }
         }
        }});

    ws.on("close", ()=>{

        for (var i = 0; i < roomList.length; i++)
         {
           for (var j = 0; j < roomList[i].wsList.length; j++)
            {
             if(roomList[i].wsList[j] == ws)
              {
                console.log(roomList[i].nameList[j] + " disconnected.");
                checkMessage = 3;
                SendAll(roomList[i].nameList[j],checkMessage,yourWS);
                roomList[i].wsList.splice(j,1);
                roomList[i].nameList.splice(j,1);

                if(roomList[i].wsList.length <= 0)
                {
                    console.log("Delete Room: " + roomList[i].roomName);
                    roomList.splice(i,1);
                }

                break;
              }
          }
        }

        for(var i = 0; i < wsList.length; i++)
        {
            if(wsList[i] == ws)
            {
                console.log(nameList[i] + " disconnected.");
                checkMessage = 3;
                SendAll(nameList[i],checkMessage);
                wsList.splice(i,1);
                nameList.splice(i,1);
                break;
            }
        }
    });
});

function SendAll(data,checkMessage,ws)
{
    for (var i = 0; i < roomList.length; i++)
    {
      for (var j = 0; j < roomList[i].wsList.length; j++)
      {
        if(roomList[i].wsList[j] == ws)
        {
           sendRoom = i;
           break;
        }
      }
    }

    for(var i = 0; i < roomList[sendRoom].wsList.length; i++)
    { 
       var sendRoomStatus = checkMessage + ";" + data;
       roomList[sendRoom].wsList[i].send(sendRoomStatus);
    }
}