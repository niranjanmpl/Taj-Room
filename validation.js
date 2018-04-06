<!--
/* =====================================================

Cute JS functions for Form Validations

========================================================= */

function isEmpty(str, Label)
{
  if(isWhitespace1(str, Label)) { return true; }
	if((str.value == null) || (str.value.length == 0 ) || (str.value.length == 'Room number')  || (str.value.length == 'Password') )
	{
    alert(Label + ' can\'t be empty or with whitespaces alone');    
    str.select();str.focus();
    return true;
	}
	return false;
}

 function isEmptyNew(str, Label) {
                       //if (str.value == null) { alert(str.length) }
                       if ((str == null) || (str.length == 0)) {
                              alert(Label + ' can\'t be empty');
                               str.select(); str.focus();
                               return true;
                           
                       }
                       return false;
                       
                   }


function isEmptyWithText(str, Label)
{
  if(isWhitespace1(str, Label)) { return true; }
	if((str.value == null) || (str.value == 'Room number') || (str.value == 'Password') || (str.value == 'Last Name') || (str.value == 'Confirm Password') || (str.value.length == 0 ))
	{
   // alert(Label + ' can\'t be empty or with whitespaces alone');    
    alert(Label);
    str.select();str.focus();
    return true;
	}
	return false;
}

//If there is atleast one whitespace found the fn will return true else false

function isWhitespace1(str,label)
{
  var spaces = " \n\t\r"
  var i;
  for(i=0;i<str.value.length;++i)
  {
	 if (spaces.indexOf(str.value.charAt(i)) == -1){ return false; }
	}
  //alert(label + " can't be empty");
  alert(label);
  str.select();str.focus();
  return true;
}

function isOnlyNumbers(str, Label)
{
 var ValidChars = "0123456789";
 var Char;
 for (i = 0; i < str.value.length; i++)
 {
  Char = str.value.charAt(i);
  if(ValidChars.indexOf(Char) == -1)
  {
     alert(Label + ' should contain only numbers');
     str.focus();
     return true;
  }
 }
 return false;
}



function isWhitespace(str)
{
  var spaces = " \n\t\r"
  var i;
  for(i=0;i<str.value.length;++i)
  {
	 if (spaces.indexOf(str.value.charAt(i)) == -1){ return true; }
	}
  return false;
}

function isEmpty1(str, Label)
{
	if((str.value == null) || (str.value.length == 0 ))
	{
    alert(Label + ' can\'t be empty');
    str.focus();
    return true;
	}
	return false;
}

function isText(str, Label)
{
 for (var i = 0; i < str.value.length; i++)
 {
  var ch = str.value.substring(i, i + 1);
  if((ch < "a" || ch > "z") && (ch < "A" || ch > "Z"))
  {
    alert(Label + ' should contain only Letters');
    str.focus();
    return false;
  }
 }
 return true;
}

function isNumeric(str, Label)
{
 for (var i = 0; i < str.value.length; i++)
 {
  var ch = str.value.substring(i, i + 1);
  if((ch < "0" || ch > "9"))
  {
    alert(Label + ' should contain only numbers');
    str.focus();
    return true;
  }
 }
 return false;
}

function isAlphaNumeric(str, Label)
{
 var ValidChars = "abcdefghijklmnopqrstuvwxyzABCEDFGHIJKLMNOPQRSTUVWXYZ0123456789@#$ ";
 var Char;
 for (i = 0; i < str.value.length; i++)
 {
  Char = str.value.charAt(i);
  if(ValidChars.indexOf(Char) == -1)
  {
     alert(Label + ' should contain only alphanumeric characters');
     str.focus();
     return true;
  }
 }
 return false;
}
function isAlphabet(str, Label)
{
 var ValidChars = "abcdefghijklmnopqrstuvwxyzABCEDFGHIJKLMNOPQRSTUVWXYZ '";
 var Char;
 for (i = 0; i < str.value.length; i++)
 {
  Char = str.value.charAt(i);
  if(ValidChars.indexOf(Char) == -1)
  {
     alert(Label + ' should contain only alphabet characters');
     str.focus();
     return true;
  }
 }
 return false;
}

function OnlyText(str, Label)
{
 var ValidChars = "abcdefghijklmnopqrstuvwxyzABCEDFGHIJKLMNOPQRSTUVWXYZ' ";
 var Char;
 for (i = 0; i < str.value.length; i++)
 {
  Char = str.value.charAt(i);
  if(ValidChars.indexOf(Char) == -1)
  {
     alert(Label + ' should contain only alphabet characters');
     str.focus();
     return true;
  }
 }
 return false;
}

function isAlphaNumeric1(str, Label)
{
 var ValidChars = "abcdefghijklmnopqrstuvwxyzABCEDFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
 var Char;
 for (i = 0; i < str.value.length; i++)
 {
  Char = str.value.charAt(i);
  if(ValidChars.indexOf(Char) == -1)
  {
     alert(Label + ' should contain only alphanumeric with/without space characters');
     str.select();
     str.focus();
     return false;
  }
 }
 return true;
}

function isEmail(str) {
    var s = str;
    var i = 1, Length = s.length, result;
    while ((i < Length) && (s.charAt(i) != '@')) i++;
    if ((i == Length) || (s.charAt(i) != '@')) {
        alert("Email address don\'t have the character @ after the login name.");
        str.focus();
        return true;
    }
    i += 2;
    while ((i < Length) && (s.charAt(i) != '.')) i++;
    if ((i == Length) || (s.charAt(i) != '.')) {
        alert("Email address don\'t have the character . after the domain name.");
        str.focus();
        return true;
    }
    if (i + 1 >= Length) {
        alert("Email address should have atleast one character after . ");
        str.focus();
        return true;
    }
    return false;
}


 function isMobileNo(str, Label)
{
 var ValidChars = "0123456789";
 var Char;
 var char1;
 //alert(str.length);
 char1 = str.charAt(0);
 //alert(char1);
 for (i = 0; i < str.length; i++) {
     Char = str.charAt(i);
     if (ValidChars.indexOf(Char) == -1) {
         alert(Label + 'Mobile No. invalid.');
         str.focus();
         return true;
         break;
     }
 }

  if (char1 == '0')
  {

     alert(Label + 'ZERO not allowed in front of mobile no.');
     str.focus();
     return true;
  }

  if (char1 == '+')
  {

     alert(Label + '+ not allowed in front of mobile no.');
     str.focus();
     return true;
 }


 if (str.length != 10)
 {
     alert(Label + 'Mobile no. should be 10 digits.');
     str.focus();
     return true;
 }

 return false;
}


 function isMobileNoInt(str, Label)
{
 var ValidChars = "0123456789";
 var Char;
 var char1;

  char1 = str.charAt(0);

  if (char1 == '0')
  {

     alert(Label + ' filed ZERO not allowed.');
     str.focus();
     return true;
  }

  if (char1 == '+')
  {

     alert(Label + ' field + not allowed.');
     str.focus();
     return true;
  }

 if (str.length > 15)
 {
   alert(Label + ' should be 5 to 15 digits.');
     str.focus();
     return true;
 }

 if (str.length < 5)
 {
   alert(Label + ' should be 5 to 15 digits.');
     str.focus();
     return true;
 }


 for (i = 0; i < str.length; i++)
 {
  Char = str.charAt(i);
  if(ValidChars.indexOf(Char) == -1)
  {
     alert(Label + ' invalid.');
     str.focus();
     return true;
  }
 }
 return false;
}

function trim(Val)
{
	while(''+Val.charAt(0)==' ')
	Val=Val.substring(1,Val.length);
	return Val
}

function ltrim(s)
{
  var i = 0;
  while ((i < s.length) && charInString (s.charAt(i), whitespace))
  i++;
  return s.substring (i, s.length);
}

// onblur="Cap_Words(this)"

function Cap_Words(obj)
{
  val = obj.value;
  newVal = '';
  val = val.split(' ');
  for(var c=0; c < val.length; c++)
  {
    newVal += val[c].substring(0,1).toUpperCase() + val[c].substring(1,val[c].length) + ' ';
  }
  obj.value = trim(newVal);
}

//This function checks the matching of Passwords.
function isMatch(pwd1, pwd2, Label)
{
	if(trim(pwd1.value)!= trim(pwd2.value))
	{
		alert(Label + " should match.")
		pwd1.focus();
		return false
	}
	return true
}

function isPhone(str, Label)
{
 var ValidChars = "0123456789()- ";
 var Char;
 for (i = 0; i < str.value.length; i++)
 {
    Char = str.value.charAt(i);
    if(ValidChars.indexOf(Char) == -1)
    {
       alert('Invalid Chars in '+ Label + ' field');
       str.focus();
       return false;
    }
 }
 return true;
}

function isFax(str, Label)
{
 var ValidChars = "0123456789()- ";
 var Char;
 for (i = 0; i < str.value.length; i++)
 {
    Char = str.value.charAt(i);
    if(ValidChars.indexOf(Char) == -1)
    {
       alert('Invalid Chars in '+ Label + ' field');
       str.focus();
       return false;
    }
 }
 return true;
}

function BoxesChecked(form, eltname)
{

  a = 0;
   for(var i = 0; i < form.elements.length; i++)
  {
   var e = form.elements[i];
   if(e.type == "checkbox" && e.name == eltname && e.checked)
   {
    a++;
   }
  }
  return a;
}

function RadioCheck(form, eltname)
{
  a = 0;
  for(var i = 0; i < form.elements.length; i++)
  {
   var e = form.elements[i];
   if(e.type == "radio" && e.name == eltname && e.checked)
   {
    a++;
   }
  }
  return a;
}

function ItemsSelected(form, eltname)
{
  a = 0;
  for(var i = 0; i < form.elements.length; i++)
  {
   var e = form.elements[i];
   if(e.type == "select-multiple" && e.name == eltname)
   {
    for(j = 0; j < e.options.length; j++)
    {
      if(e.options[j].selected)
      {
      //alert(e.options[j].text)
      //alert(e.options[j].value)
        a++;
      }
    }
   }
  }
  return a;
}

//  Check All Check boxes
//  onclick="Checkall(this, document.form, 'cbox[]');"

function Checkall(chk, form, eltname)
{
 for (var i=0; i < form.elements.length; i++)
 {
  var e = form.elements[i];
  if (e.type == "checkbox" && e.name == eltname){ e.checked = chk.checked;}
 }
}

function chkListbox(item, Label)
{
  if(item.options.selectedIndex == 0 || item.options.selectedIndex == -1)
  {
    alert('Please select ' + Label);
    item.focus();
    return true;
  }
  return false;
}

function getRadioValue(radio)
{
 for (var i = 0; i < radio.length; i++)
 {
 	if (radio[i].checked) { break; }
 }
 return radio[i].value;
}

//if(form.elements['toinv[]'].options.selectedIndex == -1) { alert('Pls select atleast one invoice'); return false; }

function chkListboxMultiple(form, eltname, Label)
{
 for(var i = 0; i < form.elements.length; i++)
 {
  var e = form.elements[i];
  if(e.type == "select-multiple" && e.name == eltname)
  {
	 if(e.options.selectedIndex == -1)
   {
     alert('Please select atleast one ' + Label);
     return false;
   }
   else
   {
    return true;
   }
  }
 }
}

//	onClick="transfer(toinv,frominv);"

function transfer(src,dest)
{
	for(i=0;i<src.options.length;i++)
	{
		o=src.options[i];
		if(o.selected)
		{
			var tmpOption=new Option;
			tmpOption.value=o.value;
			tmpOption.text=o.text;
			dest.options[dest.options.length]=tmpOption;
			src.options[i--]=null;
		}
	}
}

/*
//	onClick="transfer(act,this.form);"

function transfer(act,form)
{
 if(act == 'add')
 {
	src = form.elements['frominv[]'];
	dest = form.elements['toinv[]'];
 }
 else
 {
	src = form.elements['toinv[]'];
	dest = form.elements['frominv[]'];
 }
	for(i=0;i<src.options.length;i++)
	{
		o=src.options[i];
		if(o.selected)
		{
			var tmpOption=new Option;
			tmpOption.value=o.value;
			tmpOption.text=o.text;
			dest.options[dest.options.length]=tmpOption;
			src.options[i--]=null;
		}
	}
}
*/

function replaceAll(a,b,c)
{
	var ret="";
	var i=0;
	if(a.indexOf(b)==-1)
		return a;
	for (i=0;i<a.length;)
	{
		oldi=i;
		if((i=a.indexOf(b,oldi))==-1)
		{
			ret+=a.substring(oldi);
			break;
		}
		else
		{
			ret+=a.substring(oldi,i)+c;
			i+=b.length;
		}
	}
	return ret;
}

function validFile(file, Label)
{
  TImage = file.value
	if(TImage == "")
	{
    alert('Please select file for ' + Label);
		file.focus();
		return false;
	}
	if(TImage != "")
	{
		MyFile = TImage
		FileArray = MyFile.split("\\")
		FileName = FileArray[FileArray.length-1]
		ExtArray = FileName.split(".")
		Ext = ExtArray[ExtArray.length-1]
		Ext = Ext.toUpperCase(Ext)
		if(!(Ext=="GIF" || Ext=="JPG" || Ext=="JPE" || Ext=="PNG" || Ext=="BMP" || Ext=="SWF"))
		{
			alert("Invalid upload file!... upload the image/flash file only");
      			file.focus();
			return false;
		}
	}
	return true;
}

function isAmount(str, Label)
{
 var ValidChars = "0123456789.";
 var Char;
 for (i = 0; i < str.value.length; i++)
 {
    Char = str.value.charAt(i);
    if(ValidChars.indexOf(Char) == -1)
    {
       alert('Invalid Chars in '+ Label + ' field');
       str.focus();
       return false;
    }
 }
 return true;
}

function isValidAmount(str, Label)
{
 var ValidChars = "0123456789.";
 var Char;
 for (i = 0; i < str.value.length; i++)
 {
    Char = str.value.charAt(i);
    if(ValidChars.indexOf(Char) == -1)
    {
       alert('Invalid chars in '+ Label + ' field');
       str.focus();
       return false;
    }
 }

	AmtArray = str.value.split(".")
	if(AmtArray.length > 2)
	{
		alert("Invalid "+Label+".. "+Label+" must have single period for float.")
		str.focus();
		return false;
	}
	if(AmtArray.length > 1)
	{
		FloatAmt = AmtArray[1];
		if(FloatAmt.length > 2)
		{
			alert("Invalid "+Label+"..  float value must have two digits.")
			str.focus();
			return false;
		}
	}
	return true
}

function splitText(theNotes)
{
	theString = theNotes.split("\n")
	NewString = ""
	for(i=0; i < theString.length; i++)
	{
		NewString+=theString[i]+"|"
	}
	return NewString
}

function floatRound(number,X)
{
	X = (!X ? 2 : X);
	return Math.round(number*Math.pow(10,X))/Math.pow(10,X);
}

function daysInFebruary (year)
{
  return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
}

function DaysArray(n)
{
	for (var i = 1; i <= n; i++) {
		this[i] = 31
		if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}
		if (i==2) {this[i] = 29}
   }
   return this
}

var dtCh= "/";
var minYear=1900;
var maxYear=2100;

function isDate(dtStr,Label)
{
	var daysInMonth = DaysArray(12)
	var pos1=dtStr.indexOf(dtCh)
	var pos2=dtStr.indexOf(dtCh,pos1+1)
	var strMonth=dtStr.substring(0,pos1)
	var strDay=dtStr.substring(pos1+1,pos2)
	var strYear=dtStr.substring(pos2+1)
	strYr=strYear
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
	for (var i = 1; i <= 3; i++) {
		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
	}
	month=parseInt(strMonth)
	day=parseInt(strDay)
	year=parseInt(strYr)
	if (pos1==-1 || pos2==-1){
		alert("The date format should be : mm/dd/yyyy for "+Label + ".")
		dtStr.focus();
		return false
	}
	if (strMonth.length<1 || month<1 || month>12){
		alert("Please enter a valid month for "+Label+"\nDate Format is mm/dd/yyyy.")
		dtStr.focus();
		return false
	}
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		alert("Please enter a valid day for "+Label+"\nDate Format is mm/dd/yyyy.")
		dtStr.focus();
		return false
	}
	if (strYear.length != 4 || year==0 || year<minYear || year>maxYear){
		alert("Please enter a valid 4 digit year between "+minYear+" and "+maxYear+" for "+Label+"\nDate Format is mm/dd/yyyy.")
		dtStr.focus();
		return false
	}
	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false){
		alert("Please enter a valid date for "+Label+"\nDate Format is mm/dd/yyyy.")
		dtStr.focus();
		return false
	}
return true
}

function DateDiff(obj2,obj1)
{
// obj2 is Date object
// obj1 is the text object having the date value
// will return + value if obj2 < obj1
// will return - value if obj2 > obj1
// will return 0 if both are equal.

		var cal1 = obj1.value;
		var Date1;
		var calMode = 0;

		if (cal1.length == 10)
		{
			if(calMode == 0)
			{
				Day1 = cal1.substr(3,2)
				Month1 = cal1.substr(0,2);
			}
			else
			{
				Day1 = cal1.substr(0,2)
				Month1 = cal1.substr(3,2);
			}
			Year1 = cal1.substr(6,4);

			Date1 = new Date(Year1,Month1-1,Day1);

			return (Date1 - obj2);
		}

}

var alphabet = "abcdefghijklmnopqrstuvwxyz";
var numeric  = "0123456789";

function isValidwithBag(name,Bag)
{
	var i;
	name = name.toLowerCase();
	for(i=0;i<name.length;++i)
		if (Bag.indexOf(name.charAt(i)) == -1)
			return false;
	return true;
}

function isLogin(s)
{
	if (s.length < 5)
	{
		alert("min. length of Login ID is 5");
		return false;
	}

	if (!isValidwithBag(s,alphabet + numeric + "-._"))

	{
		alert("Login ID should contain only the characters from alphabet, numbers and '-._'");
		return false;
	}
	if (!isValidwithBag(s.charAt(s.length-1),alphabet + numeric))
	{
		alert("Login ID should end with an alphanumeric characters.");
		return false;
	}
	if (!isValidwithBag(s.charAt(0),alphabet + numeric))
	{
		alert("Login ID should start with an alphanumeric characters.");
		return false;
	}
	return true;
}

function isPassword(s)
{
	if (s.length < 5)
	{
		alert("min. length of password is 5");
		return false;
	}

	if (isWhitespace(s))
	{
		alert("please enter the password without spaces");
		return false;
	}
	return true;
}

function checkUrl(str, label)
{
	var net = "http://";
	var Flag;
	var a = str.value;
	for(i=0;i<7;i++)
	{
		if(net.charAt(i)!= a.charAt(i))
			Flag=1;
		else
			Flag=0;
	}
	if(Flag==1)
	{
		alert("URL should start with http://");
		str.focus();
		return false;
	}
	return true;
}

function round (n) {
  n = n - 0; // force number
   d = 0;
  var f = Math.pow(10, d);
  n += Math.pow(10, - (d + 1)); // round first
  n = Math.round(n * f) / f;
  n += Math.pow(10, - (d + 1)); // and again
  n += ''; // force string
  return d == 0 ? n.substring(0, n.indexOf('.')) :
      n.substring(0, n.indexOf('.') + d + 1);
}
function isValidAmount1(str,Label)
{
 var ValidChars = "0123456789.";
 var Char;
 if (str.value == '')
 {
 alert('Fixed/Margin should not have null value');
 return false;
 }
 for (j = 0; j < str.value.length; j++)
 {
    Char = str.value.charAt(j);
    if(ValidChars.indexOf(Char) == -1)
    {
       alert('Invalid chars in '+ Label + ' field');
	   str.focus();
       return false;
    }
 }
	AmtArray = str.value.split(".");
	if(AmtArray.length > 2)
	{
		alert("Invalid "+Label+".. "+Label+" must have single period for float.");
		str.focus();
		return false;
	}
	if(AmtArray.length > 1)
	{
		FloatAmt = AmtArray[1];
		if((FloatAmt.length > 2) || (FloatAmt.length == 0))
		{
			alert("Invalid "+Label+"..  float value must have two digits.")
		    str.focus();
			return false;
		}
	}
	return true;
}


function isValidAmount2(str, Label)
{
 var ValidChars = "0123456789.";
 var Char;
 for (i = 0; i < str.value.length; i++)
 {
    Char = str.value.charAt(i);
    if(ValidChars.indexOf(Char) == -1)
    {
       alert('Invalid chars in '+ Label + ' field');
       str.focus();
       return false;
    }
 }

	AmtArray = str.value.split(".")
	if(AmtArray.length > 2)
	{
		alert("Invalid "+Label+".. "+Label+" must have single period for float.")
		//str.focus();
		return false;
	}
	if(AmtArray.length > 1)
	{
		FloatAmt = AmtArray[1];
		if(FloatAmt.length > 2)
		{
			alert("Invalid "+Label+"..  float value must have two digits.")
			//str.focus();
			return false;
		}
	}
	return true
}
-->
