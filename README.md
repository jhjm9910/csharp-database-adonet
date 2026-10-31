# C# Database Programming (ADO.NET · SQL Server)

C# ADO.NET과 SQL Server로 만든 **DB 연동 애플리케이션** 모음입니다.
연결형(SqlCommand) · 비연결형(DataAdapter/DataSet) 모델, 저장 프로시저, 콘솔·GUI 관리 프로그램을 구현했습니다.

## 대표 결과물

| 프로젝트 | 내용 | 기술 |
|---|---|---|
| **계좌관리 프로그램 (DB 연동)** <br>`01_*/0415_Account_DB` | 콘솔 계좌관리 프로그램의 저장소를 SQL Server로 전환. 계좌 개설·입출금·이체·조회·삭제를 DB 접근 클래스(MyDB)로 분리 (약 1,000줄) | ADO.NET, SQL Server |
| **학생 성적 관리 (실습평가)** <br>`02_*/0417_실습평가_콘솔기반`, `0417_실습평가_GUI기반` | 학생·과목·성적 테이블을 다루는 성적 관리 프로그램을 콘솔과 WinForm 두 버전으로 구현 | ADO.NET, DataAdapter, WinForms |
| **거래내역 · 저장 프로시저** <br>`02_*/0415_DB_프로시저`, `0416_DB_거래내역포함` | 고객·상품·판매 테이블 CRUD를 저장 프로시저로 처리하고 거래내역 관리 추가 | T-SQL, Stored Procedure |
| **메모리 DB · DataAdapter** <br>`02_*/0417_아답터` | DataSet을 메모리 DB로 사용하고 DataAdapter로 SQL Server와 동기화하는 WinForm 계좌 관리 | DataSet, DataAdapter |

## 폴더별 학습 내용

| 폴더 | 내용 |
|---|---|
| `01_ADO.NET_DB기초` | DB 연결·CRUD 기초, 메모리 DB, 계좌관리 DB 연동, 실습 SQL 쿼리 |
| `02_ADO.NET_프로시저_어댑터_실습평가(0415-0417)` | 저장 프로시저, 거래내역, DataAdapter, 학생관리, 실습평가(콘솔/GUI) |
| `03_학생관리프로그램(0415)` | 학생관리 프로그램 DB 연동 버전 |

## 실행 방법

DB 접속 정보는 `MyDB.cs` 등의 연결 문자열에 있습니다. 공개 저장소라서 비밀번호는 `YOUR_PASSWORD`, 서버 이름은 `localhost`로 바꿔 두었으니 본인 SQL Server 환경에 맞게 수정하세요.

## 개발 환경

- C#, ADO.NET, SQL Server Express, WinForms — Visual Studio 2022
