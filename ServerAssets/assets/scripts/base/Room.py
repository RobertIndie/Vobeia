# -*- coding: utf-8 -*-
import KBEngine
from KBEDebug import *

class Room(KBEngine.Entity):
    """
    房间
    """
    def __init__(self):
        KBEngine.Entity.__init__(self)
        INFO_MSG("Init Room %s(%d)" % (self.RoomName,self.RoomID))

    def requestEnterRoom(self,player):
        if not player in self.Players:
            self.Players.append(player)
            player.onEnterRoomResult(0,self)
        else:
            player.onEnterRoomResult(1,None)
        
    def requestExitRoom(self,player):
        if player in self.Players:
            self.Players.remove(player)
