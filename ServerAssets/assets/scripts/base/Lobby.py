# -*- coding: utf-8 -*-
import KBEngine
from KBEDebug import *

class Lobby(KBEngine.Entity):
    """
    大厅
    1.账号进入和退出
    2.房间创建和销毁
    """
    def __init__(self):
        KBEngine.Entity.__init__(self)
        INFO_MSG("Init Lobby %s" % self.LobbyName)

        self.players = []
        self.rooms = []

        self.tempRoomID = 0

    def enterLobby(self,player):
        if player in self.players:
            return
        self.players.append(player)

    def exitLobby(self,player):
        if not player in self.players:
            return
        self.players.remove(player)

    def requestCreateRoom(self,player,roomName):
        tempRoomID = tempRoomID + 1
        params = {
            "ID":tempRoomID,
            "Name":roomName,
            "Lobby":self,
            "Players":[],
            "Creator":player
        }
        KBEngine.createEntityAnywhere("Room",params,onCreateRoomCallback)

    def onCreateRoomCallback(self,room):
        room.Creator.onCreateRoomResult(0,room)
