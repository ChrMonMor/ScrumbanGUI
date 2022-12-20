<script setup>
import Projects from './components/Projects.vue'
import axios from "axios";
</script>
<script>
export default {
  data() {
    return {
      posts: null,
      mainProject: 1,
      projectURL : variables.API_URL+'Projects',
      trashcan : `<path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5Zm-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5ZM4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06Zm6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528ZM8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5Z"/>`,
      trashOff : true,
      projectsNewName : "placeholder",
      showV : false,
    };
  },
  mounted:function() {
    this.refreshData();
  },
  methods: {
    openProject(tabname, event) {
      var i, tabcontent, tablinks;
      tabcontent = document.getElementsByClassName("tabcontent");
      for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
      }
      tablinks = document.getElementsByClassName("tablinks");
      for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
      }
      document.getElementById(tabname).style.display = "block";
      event.currentTarget.className += " active";
    },
    refreshData(){
      axios.get(this.projectURL)
      .then(response => (this.posts = response.data, this.refreshData()))
    },
    createNewProject(){
      axios.get(variables.API_URL+"Projects/Create/"+this.projectsNewName)
      .then((response) => 
      {
        this.refreshData();
        this.posts = response.data;
        this.projectsNewName = "placeholder";
        document.getElementById("showEdit").style.display = "none";
        document.getElementById("showName").style.display = "block";
      })
    },
    deleteClick(id){
      if(!confirm("Are you sure you want to delete this project?")){
        return;
      }
      axios.get(variables.API_URL+"Projects/delete/"+id)
      .then(response => (this.refreshData(),this.posts = response.data))
    },
    updateClick(id){
      axios.get(variables.API_URL+"Projects/Update/"+id+"/"+this.projectsNewName)
      .then((response) => {
        this.refreshData();
        this.posts = response.data;
        this.projectsNewName = "placeholder";
        document.getElementById("showEdit"+id).style.display = "none";
        document.getElementById("showName"+id).style.display = "block";
      })
    },
    updateOptionClick(id, name){
      document.getElementById("showEdit"+id).style.display = "block";
      document.getElementById("showName"+id).style.display = "none";
      this.projectsNewName = name;
      document.getElementById("showEdit"+id).focus();
    },
  },
}

</script>
<template>
    <ul class="tab">
      <li v-for="n in posts" :key="n.Project_Id" >
        <div v-bind:id="'showName'+n.Project_Id">
          <button @dblclick="updateOptionClick(n.Project_Id, n.Name)" class="tablinks" @click="(event) => openProject(n.Name+n.Project_Id, event)">
            {{n.Name}} 
            <span id="deleteThis" @click=deleteClick(n.Project_Id)>
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x" viewBox="0 0 16 16">
              <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>
            </svg>
          </span>
          </button>
        </div><input style="display: none; width: 300px;" v-bind:id="'showEdit'+n.Project_Id" type="text" class="form-control" v-model="projectsNewName" @focusout="updateClick(n.Project_Id)" @keyup.enter="updateClick(n.Project_Id)" />
      </li>
      <li>
        <button v-bind:id="'showName'" @click="updateOptionClick('', 'Placeholder')">+ Add Project</button>
        <input  v-bind:id="'showEdit'" type="text" class="form-control" v-model="projectsNewName" @keyup.enter="createNewProject()" style="display: none;"/>
        </li>
    </ul>
    <div class="Findlastdiv">
      <div v-for="n in posts" :key="n.Project_Id"  class="tabcontent" v-bind:id=n.Name+n.Project_Id >  
        <Projects :ProjectsId=n.Project_Id :ProjectsName=n.Name></Projects>
      </div>
    </div>

</template>

<style scoped>

/* Style the tab */
.tab {
  list-style-type: none;
  overflow: hidden;
  border: 1px solid #ccc;
  background-color: #f1f1f1;
  position: relative;
  display: flex;
}

/* Style the buttons inside the tab */
.tab button {
  background-color: inherit;
  border: none;
  outline: none;
  cursor: pointer;
  padding: 14px 16px;
  transition: 0.3s;
  font-size: 17px;
}

.showEdit{
  padding: 14px 16px;
  transition: 0.3s;
  font-size: 17px;
}

/* Change background color of buttons on hover */
.tab button:hover {
  background-color: #ddd;
}

/* Create an active/current tablink class */
.tab button.active {
  background-color: #ccc;
}

/* Style the tab content */
.tabcontent {
  display: none;
  padding: 6px 12px;
  border-top: none;
  animation: fadeEffect 1s; /* Fading effect takes 1 second */
}

/* Go from zero to full opacity */
@keyframes fadeEffect {
  from {opacity: 0;}
  to {opacity: 1;}
}
:root {
    --red: hsl(0, 78%, 62%);
    --cyan: hsl(180, 62%, 55%);
    --orange: hsl(34, 97%, 64%);
    --blue: hsl(212, 86%, 64%);
    --varyDarkBlue: hsl(234, 12%, 34%);
    --varyDark: hsl(234, 12%, 24%);
    --grayishBlue: hsl(229, 6%, 66%);
    --veryLightGray: hsl(0, 0%, 96%);
    --veryLight: hsl(0, 0%, 100%);
    --weight1: 200;
    --weight2: 400;
    --weight3: 600;
}
</style>


