--
-- ���� ������������ � ������� SQLiteStudio v3.1.1 � �� ��� 18 07:08:06 2017
--
-- �������������� ��������� ������: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- �������: 
Bookkeeping
CREATE TABLE "
Bookkeeping" (ID INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, ��� TEXT NOT NULL, ����������� TEXT NOT NULL, ��������� TEXT NOT NULL, "����������� �� ������" DATE NOT NULL, ����� INTEGER NOT NULL);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (1, '��������� ������� �������� ', '������', '������� ��������', '2007-06-15', 45000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (2, '������ ������ ������������ ', '�����������', '��������', '2009-11-01', 30000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (3, '�������� �������� ����������� ', '�����������������', '�����������', '2015-02-15', 21800);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (4, '������� ����� �������� ', '������', '����������� ��������', '2009-05-20', 38000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (5, '�������� �������� ��������� ', '������', '���������', '2017-07-12', 22100);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (6, '���������� ������ �������� ', '�����������', '������� ��������', '2016-12-19', 19400);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (7, '��������� ���� ������������� ', '�����������������', '��������', '2014-04-23', 35600);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (8, '��������� ����� �������� ', '������', '������������ �������������', '2005-05-01', 55000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (9, '�������� ����� ��������� ', '������', '��������', '2017-03-13', 30400);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (10, '�������� ����� ���������� ', '������', '������� ��������', '2013-08-07', 21600);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (11, '����������� ����� ������ ', '�����������������', '��������', '2016-12-05', 32000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (12, '������� ������ ������������� ', '�����������', '�����������', '2011-06-25', 25700);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (13, '������� ����� ��������� ', '�����������', '�����������', '2012-01-29', 24000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (14, '��������� �������� �������� ', '������', '��������', '2009-08-15', 30000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (15, '������� ���� �������� ', '������', '��������', '2016-05-13', 32300);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (16, '������� ������ ���������� ', '������', '���������', '2014-07-20', 24000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (17, '�������� ����� ��������� ', '�����������������', '��������', '2011-04-15', 37900);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (18, '��������� ������� ������������ ', '�����������', '��������', '2013-03-11', 36000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (19, '������� �������� ���������� ', '������', '������� ��������', '2009-08-17', 48000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (20, '�������� ���� ������������� ', '������', '��������', '2013-05-18', 35900);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (21, '��������� ������ ���������� ', '�����������������', '������� ��������', '2012-06-20', 22000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (22, '���������� ���� �������� ', '������', '��������', '2015-11-03', 32400);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (23, '�������� �������� �������� ', '������', '�����������', '2010-04-30', 25000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (24, '����� ������ ����������� ', '�����������', '��������', '2017-05-01', 29600);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (25, '������� ���� �������� ', '�����������������', '�������������� ��������', '2008-09-13', 40000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (26, '������� ����� ��������� ', '������', '��������', '2014-07-15', 34900);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (27, '�������� ����� ��������� ', '������', '��������', '2011-04-29', 38200);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (28, '����� ������� �������� ', '�����������������', '������� ��������', '2013-12-01', 22100);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (29, '���������� ������ ���������� ', '�����������', '��������', '2015-09-13', 33200);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (30, '��������� ������� ���������� ', '�����������', '���������', '2014-06-13', 24700);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (31, '��������� ����� ��������� ', '�����������������', '�����������', '2010-06-09', 26000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (32, '�������� ���� ���������� ', '������', '������� ��������', '2015-09-18', 19700);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (33, '�������� ������� ���������� ', '������', '��������', '2011-11-10', 38500);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (34, '���������� ������� ���������� ', '������', '��������', '2012-05-28', 27000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (35, '���������� ������� ����������� ', '�����������', '������� ��������', '2017-01-20', 17200);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (36, '�������� ���� ����������� ', '�����������������', '��������', '2014-12-03', 35000);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (37, '����� �������� ����������� ', '������', '��������', '2010-08-25', 28500);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (38, '�������� �������� ��������� ', '�����������', '������� ��������', '2017-03-20', 17500);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (39, '�������� ���� ������������� ', '�����������������', '�����������', '2014-10-19', 23400);
INSERT INTO "
Bookkeeping" (ID, ���, �����������, ���������, "����������� �� ������", �����) VALUES (40, '����� ������� ��������� ', '������', '��������', '2012-08-22', 27300);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;