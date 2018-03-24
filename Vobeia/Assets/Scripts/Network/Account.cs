namespace KBEngine
{
    public interface AccountInterface
    {
        void requestGetRoomList();
        void requestGetRoom(string roomName);
        void requestEnterRoom(uint roomID);
        void requestExitRoom();
        void requestGetRoomInfo();
    }
    public class Account : Entity,AccountInterface
    {
        public static AccountInterface instance;
        public KBEDATATYPE_UNICODE Name;

        public string lobbyName;

        public override void __init__()
        {
            instance = this;
            Event.fireOut("onLoginSuccessfully", new object[] { KBEngineApp.app.entity_uuid, id, this });
            
        }

        public void enterLobby(object lobbyName)
        {
            Event.fireOut("onEnterLobby", new object[] { lobbyName });
        }

        #region 房间
        public void onReceiveRoomList(object roomList)
        {
            Event.fireOut("onReceiveRoomList", new object[] { roomList });
        }

        public void onCreateRoomResult(object resultStatus)
        {
            Event.fireOut("onCreateRoomResult", new object[] { resultStatus });
        }

        public void onEnterRoomResult(object resultStatus)
        {
            Event.fireOut("onEnterRoomResult", new object[] { resultStatus });
        }

        public void onReceiveRoomInfo(object roomDetailInfo)
        {
            Event.fireOut("onReceiveRoomInfo", new object[] { roomDetailInfo });
        }

        void AccountInterface.requestGetRoomList()
        {
            baseCall("requestGetRoomList");
        }

        void AccountInterface.requestGetRoom(string roomName)
        {
            baseCall("requestGetRoom", new object[] { roomName });
        }

        void AccountInterface.requestEnterRoom(uint roomID)
        {
            baseCall("requestEnterRoom", new object[] { roomID });
        }

        void AccountInterface.requestExitRoom()
        {
            baseCall("requestExitRoom");
        }

        void AccountInterface.requestGetRoomInfo()
        {
            baseCall("requestGetRoomInfo");
        }
        #endregion
    }
}
