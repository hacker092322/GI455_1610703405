var websocket = require('ws');

var websocketserver = new websocket.Server({port:25500}, ()=>{
    console.log("Chatroom server is running.");
});

var wsList = [];
var nameList = [];
var addName = false;
var checkMessage;
        // 0 = Message from sender
        // 1 = Message from other
        // 2 = Connect Message
        // 3 = Disconnect Message
var checkSender;

websocketserver.on("connection",(ws,rq)=>{
    addName = true;
    wsList.push(ws);

    ws.on("message", (data)=>{

        if (addName == true){
            nameList.push(data);
            console.log(data + " connected.");
            checkMessage = 2;
            SendAll(data,checkMessage);
            addName = false;
        }
        
        else 
        {
            //Sender
            for(var i = 0; i < wsList.length; i++)
            {
              if(wsList[i] == ws)
              {
                 checkSender = i;
                 console.log("Send from "+ nameList[i] +" : " + data);
                 console.log("Sender Index : " + checkSender);
                 break;
              }
            }
    
            //Boardcast
            for(var i = 0; i < wsList.length; i++)
         {
            if(i == checkSender)
            {
              checkMessage = 0;
              wsList[i].send(checkMessage);
              wsList[i].send(data);
              continue;
            }
    
            else
            {
              checkMessage = 1;
              wsList[i].send(checkMessage);
              console.log("Send to " + nameList[i]);
              wsList[i].send(data);
              wsList[i].send(nameList[checkSender]);
              console.log("Send Complete");
            }
         }
        }});

    ws.on("close", ()=>{
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

function SendAll(data,checkMessage)
{
    for(var i = 0; i < wsList.length; i++)
    {
        wsList[i].send(checkMessage);
        wsList[i].send(data);
    }
}