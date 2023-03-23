# TicTacToeAPI
 ������ API ������������� ���������� ���� � ��������-������ �� �������.

 1. ����� ���������� � ����������� API
 ��� ������ ���� ��������� ���� �� �������, � ����� ������������� ID. ���� ��������� �� ������� ����� GET ������� �� ������ "https://localhost:44310/api/TicTacToe/new".���� ���� ������� �������, �� ������ ������ � ID �����, ����� ������ ������.
 ��� ����, ����� ������ ������, ���������� ������������ � ���� - �������� �� ������ � ID ����� (����������� GET ������ �� ������ "https://localhost:44310/api/TicTacToe/{gameID}/connect") � �������� � �������� ID ����� ������ � ���� ����, ������� ������ �������� � ����� �� ������ � �����������.
 ������ ���, ������������ �� ������, �������� � ���� ��� ������������: ID ����, ID ������ � ����, ��� ���. ��� ����, ����� ����������� ��� � ����, ���������� ������� PUT ������ �� ������ "https://localhost:44310/api/TicTacToe/{gameID}/turn", ������� ��������� ��� ���������. 
 ���� ��� ������ � ������� �� ������������� ���� ��������� (���� � ����� ID ����������, ����� � ����� ID ��������� � ����, ��� ��� ������������� �������� ����), �� ���� ���� �� ������� ���������� � ������������ � �����, ����� ���� � �������� ������ �� ������ ������� ������������ ���������� ������ ����. � ��������� ������ ������������ ������.
 ����� ����, ����� ������������ ��������� ������� ������ ������� ����, ������� �� ���������� (� ���� ����� �� ������������, �� ������ � ��� ����) ���������� �����. ����� ����� ����������� �� ���� �� ������� �� ����� "https://localhost:44310/api/TicTacToe/{gameID}/disconnect", ������� ���� playerID.

 2. �������� ���������� ���������� ������ API
     2.1 ����� Turn
		������ ���������� � ���� (����� ������ �� ���� �� ����������� � �� ���������)
	 2.2 ����� GameModel 
		�������� ���������� �� ���� � �������� � ��� ������������� �����, � ����� ��������� ID ��� �������������� �������.
		�������� ����� ����� ������ �������� Field, ��� �������� ���������� � ���� ����. ��� ���� ����������� ��� List<List<int>>, � �� int[,], ��������� ����� ��������� ������ ��� ������ ���������� ������� � JSON. 
		Field ��� ������� ���� ������ ����� �� 0 �� 2: 0 - ������ ����; 1 - �����; 2 - �������. �� ������ ���� (MakeATurn) �����������, ��������� �� ����� � ����, ������������� �� ��� ��� �������� ����. ���� ��� �� ������, �� ������������ false. ����� ���� ���������� � ������������ � �����, ����������� �� ��������� �� ���. ���� ���� �����������, �� �������� ���� GameIsFinished �������� �� true.
		���� ���� ��������� ��-�� ������������ ����, �� �������� ���� FieldIsFull �������� �� true. � �������� ������ ������������ ������ GameModel, ������� ������� ������ �������� ������ ��������� ���� ����� ����, ����������� �� ���, ���� ��, �� �� ������������ ���� ��� �� ����, ��� �����-�� �� ������� �������. ����� ������ ������ �� ���������� ����� ����������� ����, ������� ���� ������ ������� ���� �� ���������� GameIsFinished=true � FieldIsFull=false, ������ ������� ���, ��� ������ ���.
		��� ������� ����������� � ���� (GenerateIDForPlayer) �����������, ���� �� ��������� ����� � ����. ���� ��, �� ������������ ID ��� ��������������� ������, ����� ������������ null.
		
		*���� ����������� ����� ������� ��������� ID: ���� 1, ���� 2. ���� ����� ��������� ID ����� ������� ��������, ��������� � ������ ����� ����� ������ � ���������� ID � ���� ������ �������� ������ ���� ID ����, �� �� ������ ������ ���� � ������ ����, ���� �� � ��� �� �����������. ����� ������� ��������� ���������� ID, � ����� ����������� � ���� �� ������.
		
		���� ����� ����������� �� ���� (LeaveTheGame), �� ���� �������� ��� ID.
	 2.3 ����� InitializeConnectionStruct
		������ ���������� �� ����, � ������� �������������� �����������, � ����������� � ���� ����������� playreID. 
     2.4 ����� GameManager
	    ��������� ������� ��� �� �������.
		DeleteOldGames() ������� ������ �������������� ���� �� �������;
		CreateNewGame() ������� ����� ���� �� ������� � ���������� �;
		FindIDForNewGame() ������� ���������������� ID ��� ����������� ����;
		GetGameByID() ���������� ����, ���������� �� �������, �� � ID;
		SaveGame() ��������� ���� � ���� �� ������� "Game[gameID]";
		DeleteGame() ������� ���� � �������, ���� ��� �� ��� ��������.
