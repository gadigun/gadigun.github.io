Unity
http://theeye.pe.kr/archives/tag/waituntil

WaitUntil ( () => _isEnd )  // _isEnd가 true이면 종료
WaitWhile( () => _isEnd ) // _isEnd가 false 이면 종료

Func<int, int> func1 = (int x) => x + 1;
Func<int, int> func2 = (int x) => { return x + 1; };

yield return new WaitUntil (System.Func<Bool> predicate);
이번엔 특정 조건식이 성공할때까지 기다리는 방법입니다.

yield return new WaitWhile(System.Func<Bool> predicate);
WaitWhile은 false가 될때까지 기다립니다.

yield return new WaitForSecondsRealtime (float time);
WaitForSeconds와 하는 역할은 동일하지만 결정적으로 다른것이 있습니다. 유니티상의 시간은 임의로 느리게 하거나 빠르게 하는 것이 가능합니다. 이를 Time.timeScale을 통해서 조정을 할 수 있습니다. 매트릭스에서 보던 총알이 느리게 날아오면서 그것을 피하는 모션을 구현해 본다면 이 값을 1보다 낮추게 되면 현재 시간의 진행보다 느려지게 되며 1보다 빠르게 변경하면 현재의 시간의 진행보다 빨라지게 됩니다. 하지만 WaitForSecondsRealtime는 이러한 Scaled Time의 영향을 받지 않고 현실 시간 기준으로만 동작을 하게 됩니다.

yield return new WaitForFixedUpdate ();
다음 FixedUpdate() 가 실행될때까지 기다리게 됩니다. 이 FixedUpdate()는 Update()와 달리 일정한 시간 단위로 호출되는 Update() 함수라고 생각하시면 됩니다.

yield return new WaitForEndOfFrame ();
하나의 프레임워 완전히 종료될 때 호출이 됩니다. Update(), LateUpdate() 이벤트가 모두 실행되고 화면에 렌더링이 끝난 이후에 호출이 됩니다. 특수한 경우에 사용하면 될 것 같습니다만 잘 모르겠군요.


yield return null;
WaitForEndOfFrame를 이야기 했다면 이것을 꼭 이야기 해야 할 것 같습니다. yield return null; 을 하게 되면 다음 Update() 가 실행될때까지 기다린다는 의미를 갖게 됩니다. 좀 더 정확하게는 Update()가 먼저 실행되고 null을 양보 반환했던 코루틴이 이어서 진행 됩니다. 그 다음에 LateUpdate()가 호출됩니다.

yield return StartCoroutine (IEnumerator coroutine);
이번에는 심지어 코루틴 내부에서 또다른 코루틴을 호출할 수 있습니다. 물론 그 코루틴이 완료될 때까지 기다리게 됩니다. 의존성 있는 여러작업을 수행하는데에 유리하게 사용 될 수 있습니다.



String Format for Int [C#]
Integer numbers can be formatted in .NET in many ways. You can use static method String.Format or instance method int.ToString. Following examples shows how to align numbers (with spaces or zeroes), how to format negative numbers or how to do custom formatting like phone numbers.

Add zeroes before number
To add zeroes before a number, use colon separator ?:“and write as many zeroes as you want.

[C#]
String.Format("{0:00000}", 15);          // "00015"
String.Format("{0:00000}", -15);         // "-00015"

Align number to the right or left
To align number to the right, use comma ?,“ followed by a number of characters. This alignment option must be before the colon separator.

[C#]
String.Format("{0,5}", 15);              // "   15"
String.Format("{0,-5}", 15);             // "15   "
String.Format("{0,5:000}", 15);          // "  015"
String.Format("{0,-5:000}", 15);         // "015  "

Different formatting for negative numbers and zero
You can have special format for negative numbers and zero. Usesemicolon separator ?;“ to separate formatting to two or three sections. The second section is format for negative numbers, the third section is for zero.

[C#]
String.Format("{0:#;minus #}", 15);      // "15"
String.Format("{0:#;minus #}", -15);     // "minus 15"
String.Format("{0:#;minus #;zero}", 0);  // "zero"

Custom number formatting (e.g. phone number)
Numbers can be formatted also to any custom format, e.g. like phone numbers or serial numbers.

[C#]
String.Format("{0:+### ### ### ###}", 447900123456); // "+447 900 123 456"
String.Format("{0:##-####-####}", 8958712551);       // "89-5871-2551"

출처 http://www.csharp-examples.net/examples/
[출처] c# String.Format && string 자릿수 맞추기|작성자 똥이꼼

Unity
http://theeye.pe.kr/archives/tag/waituntil

WaitUntil ( () => _isEnd )  // _isEnd가 true이면 종료
WaitWhile( () => _isEnd ) // _isEnd가 false 이면 종료

Func<int, int> func1 = (int x) => x + 1;
Func<int, int> func2 = (int x) => { return x + 1; };

yield return new WaitUntil (System.Func<Bool> predicate);
이번엔 특정 조건식이 성공할때까지 기다리는 방법입니다.

yield return new WaitWhile(System.Func<Bool> predicate);
WaitWhile은 false가 될때까지 기다립니다.

yield return new WaitForSecondsRealtime (float time);
WaitForSeconds와 하는 역할은 동일하지만 결정적으로 다른것이 있습니다. 유니티상의 시간은 임의로 느리게 하거나 빠르게 하는 것이 가능합니다. 이를 Time.timeScale을 통해서 조정을 할 수 있습니다. 매트릭스에서 보던 총알이 느리게 날아오면서 그것을 피하는 모션을 구현해 본다면 이 값을 1보다 낮추게 되면 현재 시간의 진행보다 느려지게 되며 1보다 빠르게 변경하면 현재의 시간의 진행보다 빨라지게 됩니다. 하지만 WaitForSecondsRealtime는 이러한 Scaled Time의 영향을 받지 않고 현실 시간 기준으로만 동작을 하게 됩니다.

yield return new WaitForFixedUpdate ();
다음 FixedUpdate() 가 실행될때까지 기다리게 됩니다. 이 FixedUpdate()는 Update()와 달리 일정한 시간 단위로 호출되는 Update() 함수라고 생각하시면 됩니다.

yield return new WaitForEndOfFrame ();
하나의 프레임워 완전히 종료될 때 호출이 됩니다. Update(), LateUpdate() 이벤트가 모두 실행되고 화면에 렌더링이 끝난 이후에 호출이 됩니다. 특수한 경우에 사용하면 될 것 같습니다만 잘 모르겠군요.


yield return null;
WaitForEndOfFrame를 이야기 했다면 이것을 꼭 이야기 해야 할 것 같습니다. yield return null; 을 하게 되면 다음 Update() 가 실행될때까지 기다린다는 의미를 갖게 됩니다. 좀 더 정확하게는 Update()가 먼저 실행되고 null을 양보 반환했던 코루틴이 이어서 진행 됩니다. 그 다음에 LateUpdate()가 호출됩니다.

yield return StartCoroutine (IEnumerator coroutine);
이번에는 심지어 코루틴 내부에서 또다른 코루틴을 호출할 수 있습니다. 물론 그 코루틴이 완료될 때까지 기다리게 됩니다. 의존성 있는 여러작업을 수행하는데에 유리하게 사용 될 수 있습니다.

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
