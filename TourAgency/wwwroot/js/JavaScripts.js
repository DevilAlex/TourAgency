function HideError()
{
    var spanelements = document.getElementsByTagName('span');
    for (var i = 0; i < spanelements.length; i++)
        spanelements[i].style.display = 'none';
}
function OnReset()
{
    var Inp = document.getElementsByTagName('input');
    for (var i = 0; i < Inp.length; i++)
        Inp.innerText = "";
    return true;
}

//Функции для формы FindTour.php
function BuyTourButton(e)
{
   var Row = e.target.name;  
   document.querySelector('#TourID').value = document.getElementsByClassName('datagrid')[0].rows[Row].cells[0].textContent;
   document.querySelector('#CountryID').value = document.getElementsByClassName('datagrid')[0].rows[Row].cells[1].textContent;
}

function ChangeSelect(e)
{
   var TrArray=document.getElementsByTagName('tr');
   for (var i = 0; i < TrArray.length; i++)
      TrArray[i].style.display = 'table-row';
   var SelectArray = ['client','manager','days','country','touroper'];
   for(var i=0; i < SelectArray.length; i++)
      if(SelectArray[i] != e.target.id)
         document.getElementById(SelectArray[i]).selectedIndex=0;
   var SelectedIndex = document.getElementById(e.target.id).selectedIndex;
   if(SelectedIndex!=0){
      var SelectedElement = document.getElementById(e.target.id)[SelectedIndex].text;
      var Table = document.getElementsByClassName('datagrid')[0];
      for(var i = 1; i<Table.rows.length; i++) 
          if (Table.rows[i].cells[e.target.name].textContent != SelectedElement) {
             document.getElementById(i).style.display = 'none';
     }
   }
}
function ResetFilters()
{
  var TrArray=document.getElementsByTagName('tr');
   for (var i = 2; i < TrArray.length; i++)
      TrArray[i].style.display = 'table-row';
   var SelectArray = ['client','manager','days','country','touroper'];
   for(var i = 0; i < SelectArray.length; i++)
      document.getElementById(SelectArray[i]).selectedIndex=0;
} 
  

function CheckManager(e)
{
   var Chk = document.getElementsByClassName('check');
    for (var i = 0; i < Chk.length; i++) {
        if (Chk[i].name != e.target.name)
            Chk[i].checked = false;
    }
    document.querySelector('#manID').value = e.target.name;
}

function OnSubmitClients()
{
   var flag = true;
   if (document.querySelector('#ButtonName').value != "Добавить"){ 
      flag = false;
      var Chk = document.getElementsByClassName('check');
      for(var i = 0; i < Chk.length; i++)  
         if(Chk[i].checked)
            flag = true;
      if(!flag)
         alert("Не выбран клиент");
   }
   return flag;
} 
function CheckClient(e)
{
   var Chk = document.getElementsByClassName('check');
   for (var i = 0; i <Chk.length; i++) 
      if(Chk[i].name != e.target.name)
         Chk[i].checked = false;
    document.querySelector('#cliID').value = e.target.name;
}


//Функции для формы HotelInfo.php

function ChooseCountry(e)
{
    document.querySelector('#ChoosenCountry').value = e.target.value;
}
//Функции для формы Statistics.php
function StatOnSubmit()
{
   document.querySelector('#ScreenHeight').value = parent.pageframe.innerHeight;
   document.querySelector('#ScreenWidth').value = parent.pageframe.innerWidth;
   return true;
}

function CountryIndex()
{
    document.querySelector("#Index").value = document.querySelector("#Country").selectedIndex;
}