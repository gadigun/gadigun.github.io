C#에서 스트링에 콤마 표시를 위해서는

ToString이나 string.Format으로

서식지정자를 이용 하면 간단 하게 숫자에 콤마 표시를 할 수 있다. 

long value =  100000000;
print(value.ToString("C"));      // 화폐단위 ₩100,000,000
print(value.ToString("C2"));     // 화폐단위 + 소숫점 2자리까리 표시 ₩100,000,000.00
print(value.ToString("N0"));     // 콤마만 표시 100,000,000
print(value.ToString("N"));      // 콤마만 표시 + 소숫점 2자리 100,000,000.00
print(value.ToString("N2"));     // 콤마만 표시 + 소숫점 2자리 100,000,000.00
print(value.ToString("#,##0")); // 콤마만 표시 100,000,000 
print(string.Format( "{0:N0}", value)); // 콤마만 표시 100,000,000
 
C 와 N은 언어와 문화권에 따라서 표기법이 자동으로 바껴서 표기 된다. 

그래서 다국어 작업 할 때 유리.

using System.Globalization;
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr-FR");
으로 변경 가능하다.
 

#,##0 는 사용자 지정 서식 지정자로 
# : 0이 앞에 붙지 않음. 해당 자리에 숫자가 있으면 표시 없으면 표시 안 함. 
0 : 0이 앞에 붙음.  해당 자리에 숫자가 있으면 숫자 표시, 숫자가 없으면 0으로 표시 함
, : 콤마 표시 
string.Format : ToString 말고 string.Format 함수로도 사용 할 수 있다.

MSDN docs.microsoft.com/ko-kr/dotnet/standard/base-types/standard-numeric-format-strings

표준 DateTime 형식에서 만족할 수 없다 날짜를 내 맘대로 표시하고 싶다 하고 싶을 경우에 어떻게 해야하는지 설명하겠다.

필요한 것은 표준 DateTime 형식으로 저장된 날짜 혹은 시간

예를 들자면
string sDate = "2012-06-20";

혹은
string sTime = "14:00";
 
같은 방식이다. 뭐 포맷은 많지만 내가 애용하는 것은 위의 두 방식이다.


사용법

현재 시각을 구하는 방식으로 예를 들어보겠다.

DateTime.Now.Tostring("포맷방식");


포맷방식 중 자주 쓰이는 것 들

2012-06-20 15:40

1. 연도 표시
yy - 12
yyyy - 2012

2. 월 표시
M - 6
MM - 06 (앞의 0 표시)

3. 일 표시
d - 20
dd - 20 (앞의 0 표시)

4. 요일 표시
ddd - 수
dddd - 수요일

5. 시 표시
h - 3
hh - 03 (앞의 0 표시)
H - 15
HH - 15 (앞의 0 표시)

6. 분 표시
m - 40
mm - 40 (앞의 0 표시)

6. 초 표시
s - 3
ss - 03 (앞의 0 표시)

7. 오후 오전 표시
tt - 오후

종합해보자면
DateTime.Now.ToString("yyyy년 MM월 dd일의 tt HH시 mm분 하고도 s초 되겠더라");

결과값
2012년 06월 20일의 오후 15시 56분 하고도 46초 되겠더라

DateTime.Now.ToString("포맷", new CultureInfo("문화권"));

예시
DateTime.Now.ToString("yyyy MMM dd (ddd) tt HH mm s", new CultureInfo("en-US"))

결과값
2012 Jun 20 (Wed) PM 16 12 36
