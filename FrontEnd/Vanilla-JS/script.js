var data;
var currentPage = 0;
var end = 10;
var total = 0;
var perPage = 10;
var authBearer = "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlByYXZlZW4iLCJUeXBlIjoiQWRtaW4iLCJuYmYiOjE2Mjg1NzcyNTgsImV4cCI6MTYyOTE4MjA1OCwiaWF0IjoxNjI4NTc3MjU4fQ.Xmm5xgA-TaCSnRpaEKlySJgVVeLlsmYT04TeHiKo2EZIJ6c6wuwcJhFjGqxFDlCWaSa4JQLA9cEI3fBy3pdKZg";
var selectedrows =[];
var typeselected;


async function showdata(sortcol='id', order='asc') {
  const res = await fetch('https://localhost:5001/api/staff', {
    method: "GET",
    headers: { "Authorization": authBearer, "Access-Control-Allow-Origin": "*" }
  });
  const d = await res.json();
  total = d.length;
  data = d;
  if(order != "asc"){
    data.sort((a, b) => a[sortcol] - b[sortcol]).reverse();
  }
  else{
    data.sort((a, b) => a[sortcol] - b[sortcol]);
  }
  
  loadTableData(data.slice(currentPage, end));
}

async function updatedata(id, updateData) {
  const res = await fetch('https://localhost:5001/api/staff/' + id, {
    method: "PUT",
    headers: { "Content-Type": "application/json", "Authorization": authBearer, "Access-Control-Allow-Origin": "*" },
    body: updateData,
  });
  showdata();
}

async function add_data(newdata){
  const res = await fetch('https://localhost:5001/api/staff/addstaff', {
    method: "POST",
    headers: { "Content-Type": "application/json", "Authorization": authBearer, "Access-Control-Allow-Origin": "*" },
    body: newdata,
  });
  showdata();
}

async function deletedata(id){
  const res = await fetch('https://localhost:5001/api/staff/' + id, {
    method: "DELETE",
    headers: { "Content-Type": "application/json", "Authorization": authBearer, "Access-Control-Allow-Origin": "*" }
  });
  showdata();
}

function sortbyusername(){
  if(event.target.innerHTML == "south"){
    showdata("userName");
    event.target.innerHTML = "north";
  }
  else{
    showdata("userName", "desc");
    event.target.innerHTML = "south";
  }  
}

function sortbyid(){
  if(event.target.innerHTML == "south"){
    showdata();
    event.target.innerHTML = "north";
  }
  else{
    showdata("id", "desc");
    event.target.innerHTML = "south";
  }  
}

function nextpage() {
  buttonvisible();
  currentPage = end;
  end = end + perPage;
  total = data.length;
  loadTableData(data.slice(currentPage, end));
}

function prevpage() {
  buttonvisible();
  currentPage = currentPage - perPage;
  end = end - perPage;
  total = data.length;
  loadTableData(data.slice(currentPage, end))
}



function loadTableData(items) {
  buttonvisible();
  const table = document.getElementById("table-body");
  table.innerHTML = "";
  items.forEach((item, i) => {
    let row = table.insertRow();
    row.setAttribute("class", "tr-class")
    row.setAttribute("id", i)

    let sel = row.insertCell(0);
    var text = "<input type='checkbox' class='checkbox' onclick='checkboxselected()' id='" + i + "'>";
    sel.innerHTML = text;

    let id = row.insertCell(1);
    id.innerHTML = item.id;
    id.setAttribute("class", "td-class")

    let name = row.insertCell(2);
    name.innerHTML = item.userName;
    name.setAttribute("class", "td-class")

    let pass = row.insertCell(3);
    pass.innerHTML = item.password;
    pass.setAttribute("class", "td-class")

    let date = row.insertCell(4);
    date.innerHTML = item.dateOfJoining;
    date.setAttribute("class", "td-class")

    let subject = row.insertCell(5);
    subject.innerHTML = item.subject;
    subject.setAttribute("class", "td-class")

    let phn = row.insertCell(6);
    phn.innerHTML = item.phoneNumber;
    phn.setAttribute("class", "td-class")

    let type = row.insertCell(7);
    type.innerHTML = item.type;
    type.setAttribute("class", "td-class")

    let edit = row.insertCell(8);
    edit.innerHTML = "<div><button onclick='editcontentinline()'><i id='icon' class='material-icons'>edit</i></button><button class='delete-btn' onclick='deleteinline()'><i id='icon' class='material-icons'>delete</i></button></div>";
    type.setAttribute("class", "td-class")

  });
}

function checkboxselected() {
  var tr = event.target.parentNode.parentNode;
  var deleteBtn = tr.children[8].children[0].children[1];
  deleteBtn.classList.toggle("delete-btn");
  console.log(selectedrows.length)
  if(event.target.checked){
    selectedrows.push(event);
  }
  else{
    selectedrows.pop(event);
  }
  if(selectedrows.length>1){
    document.getElementById("delete-selected").classList.remove("delete-selected-show");
  }
}




function deleteinline(){
  var tr = event.target.parentNode.parentNode.parentNode.parentNode;
  var id = tr.cells[1].innerHTML;
  var choice;
  if (confirm("Are you sure you want to delete this staff?")) {
    choice = "ok";
  } else {
    choice = "cancel";
  }
  console.log(choice);
  if(choice === "ok"){
    for(var i=0; i<data.length ;i++){
      if(data[i].id == id){
        tr.remove();
        deletedata(id);
      }
    }
  }
}

function editcontentinline() {
  var tr = event.target.parentNode.parentNode.parentNode.parentNode;
  var id = tr.cells[1].innerHTML;
  console.log(id)
  if (tr.className == "tr-class-edit") {
    var icon = event.target.parentNode.children[0];
    icon.innerHTML = "edit";
    tr.setAttribute("class", "tr-class");
    var values = [];
    for (let index = 2, col; col = tr.cells[index]; index++) {
      if (index != 8) {
        col.setAttribute("contenteditable", false);
        values.push(col.innerHTML);
      }
    }
    var postBody = {
      "userName": values[0],
      "password": values[1],
      "dateOfJoining": values[2],
      "subject": values[3],
      "phoneNumber": values[4],
      "type": values[5]
    }
    console.log(postBody);
    updatedata(id, JSON.stringify(postBody));
  }
  else {
    var icon = event.target.parentNode.children[0];
    icon.innerHTML = "send";
    tr.setAttribute("class", "tr-class-edit");
    for (let index = 2, col; col = tr.cells[index]; index++) {
      if (index != 8) {
        col.setAttribute("contenteditable", true);
      }
    }
  }
}

function findstaff() {
  var id = document.getElementById("staffid").value;
  console.log(id)
}

function editstaff() {
  var id = document.getElementById("staffeditid").value;
  console.log(id)
}

function buttonvisible() {
  var prevbtn = document.getElementById("prev");
  currentPage = end - perPage;
  if (currentPage <= 0) {
    prevbtn.disabled = true;
  }
  else {
    prevbtn.disabled = false;
  }
  var nextbtn = document.getElementById("next");
  currentPage = end - perPage;
  if (currentPage + perPage >= total) {
    nextbtn.disabled = true;
  }
  else {
    nextbtn.disabled = false;
  }
}

function togglepopupaddform() {
  var type = document.getElementById("select-drop");
  typeselected = event.target.value;
  type.style.display = "none";
  var form = document.getElementById("add-form");
  if (form.style.getPropertyValue('display') == "block") {
    form.style.display = "none";
  }
  else {
    form.style.display = "block"
  }
  if(typeselected!="Admin"){
    document.getElementById("subject").setAttribute("required", "true");
  }

}

function opendropdown() {
  document.getElementById("myDropdown").classList.toggle("show");
}

window.onclick = function(event) {
  if (!event.target.matches('.dropbtn')) {
    var dropdowns = document.getElementsByClassName("dropdown-content");
    for (var i = 0; i < dropdowns.length; i++) {
      var openDropdown = dropdowns[i];
      if (openDropdown.classList.contains('show')) {
        openDropdown.classList.remove('show');
      }
    }
  }
}

function filtertype(ty){
  data = data.filter(({type})=> type === ty);
  document.getElementById("clear-filter").classList.remove("clear-filter-hidden");
  loadTableData(data.slice(currentPage, end));
}

function cleartypefilter(){
  document.getElementById("clear-filter").classList.add("clear-filter-hidden");
  data = showdata("id");
}

function deleteallselected(){
  document.getElementById("delete-selected").classList.add("delete-selected-show");
  for(var i=0; i<selectedrows.length;i++){
    var tr = selectedrows[i].target.parentNode.parentNode;
    var id = tr.cells[1].innerHTML;
    for(var j=0; j<data.length ;j++){
      if(data[j].id == id){
        tr.remove();
        deletedata(id);
      }
    }
  }
}

function resetform(){
  event.preventDefault();
  document.getElementById("form").reset();
}

function onformsubmit(){
  event.preventDefault();
  var form = document.getElementById("form");
  var username = form.elements['username'].value ;
  var phone = form.elements['phone'].value;
  var password = form.elements['password'].value;   
  var date = form.elements['dateofjoin'].value;
  var subject = form.elements['subject'].value;
  var type = typeselected;

 
  if(!validateinputs("username", username)){
    alert("username not unique");
  }
  else if(!validateinputs("dateofjoin", date)){
    alert("not a valid date for Date of Joining (yyyy-mm-dd)")
  }
  else if(!validateinputs("phone", phone)){
    alert("phone number must be 10 digits")
  } 
  else{
    var dataObj =  {
      "userName": username,
      "password": password,
      "dateOfJoining": date,
      "subject": subject,
      "phoneNumber": phone,
      "type": type
    }
    
    add_data(JSON.stringify(dataObj));
    form.reset();
  }

}


function validateinputs(prop, value){
    if(prop == "username"){
      var res = data.filter(({userName})=> userName === value);
      if(res.length == 0){
        return true;
      }
      else{
        return false;
      }
    }
    else if(prop == "dateofjoin"){
      var d = new Date(value);
      if(d =="Invalid Date"){
        return false;
      }
      else{
        return true;
      }
    }
    else if(prop== "phone"){
      if(value.length !=10){
        return false;
      }
      else{
        return true;
      }
    }
} 


function addnewstaff(){
  if(document.getElementById("add-form").style.display == "block"){
    window.location.reload();
  }
 
}