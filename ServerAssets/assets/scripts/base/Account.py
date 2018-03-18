# -*- coding: utf-8 -*-
import KBEngine
from KBEDebug import *

class Account(KBEngine.Proxy):
	"""
	账号
	1.和客户端对接
	2.客户端和大厅交流的接口
	"""
	def __init__(self):
		KBEngine.Proxy.__init__(self)

		self.clientControl = True

		self.Lobby = KBEngine.globalData[self.LobbyIndex]
		self.Lobby.enterLobby(self)

	#============房间===========

	def requestGetRoomList(self):
		roomList = self.Lobby.Rooms
		result = []
		for room in roomList:
			roomInfo = {}
			roomInfo["id"] = room.ID
			roomInfo["name"] = room.Name
			roomInfo['players_count'] = len(room.Players)
			result.append(roomInfo)
		self.client.onReceiveRoomList(result)

	def requestCreateRoom(self,roomName):
		self.Lobby.requestCreateRoom(self,roomName)

	def requestEnterRoom(self,roomID):
		for room in self.Lobby.Rooms:
			if room.ID == roomID:
				room.requestEnterRoom(self)

	def requestExitRoom(self):
		self.Room.requestExitRoom(self)

	def requestGetRoomInfo(self):
		result = {}
		result["id"] = self.Room.id
		result["name"] = self.Room.Name
		players = self.Room.Players
		playersInfoList = []
		for player in players:
			playerInfo = {}
			playerInfo["name"] = player.__ACCOUNT_NAME_
			playersInfoList.append(playerInfo)
		result["players_count"] = len(playersInfoList)
		result["players"] = playersInfoList
		self.client.onReceiveRoomInfo(result)

	def onCreateRoomResult(self,status,room):
		if status == 0:
			room.requestEnterRoom(self)#创建后加入房间
		self.client.onCreateRoomResult(status)

	def onEnterRoomResult(self,status,room):
		if status == 0:
			self.Room = room
		self.client.onEnterRoomResult(status)
		
	#===========================

	def onClientEnabled(self):
		self.client.enterLobby(KBEngine.globalData[self.LobbyIndex].LobbyName)

	def onTimer(self, id, userArg):
		"""
		KBEngine method.
		使用addTimer后， 当时间到达则该接口被调用
		@param id		: addTimer 的返回值ID
		@param userArg	: addTimer 最后一个参数所给入的数据
		"""
		DEBUG_MSG(id, userArg)
		
	def onEntitiesEnabled(self):
		"""
		KBEngine method.
		该entity被正式激活为可使用， 此时entity已经建立了client对应实体， 可以在此创建它的
		cell部分。
		"""
		INFO_MSG("account[%i] entities enable. mailbox:%s" % (self.id, self.client))
			
	def onLogOnAttempt(self, ip, port, password):
		"""
		KBEngine method.
		客户端登陆失败时会回调到这里
		"""
		INFO_MSG(ip, port, password)
		return KBEngine.LOG_ON_ACCEPT
		
	def onClientDeath(self):
		"""
		KBEngine method.
		客户端对应实体已经销毁
		"""
		DEBUG_MSG("Account[%i].onClientDeath:" % self.id)
		KBEngine.globalData[self.LobbyIndex].exitLobby(self)
		if self.Room != None:
			self.Room.requestExitRoom(self)
		self.destroy()
