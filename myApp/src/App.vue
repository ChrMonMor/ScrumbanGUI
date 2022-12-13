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
      projectURL : 'https://localhost:7026/Projects'
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
                axios.get("https://localhost:7026/Projects/Create/PlaceholderName")
                .then(response => (this.refreshData(),this.items = response.data))
            },
  },
}
</script>
<template>
    <div class="tab">
          <button v-for="n in posts" :key="n.Project_Id" class="tablinks" @click="(event) => openProject(n.Name, event)">{{n.Name}}</button>
          <button @click=createNewProject()>+ Add Project</button>
    </div>
    <div v-for="n in posts" :key="n.Project_Id"  class="tabcontent" v-bind:id=n.Name> 
      <Projects :ProjectsId=n.Project_Id :ProjectsName=n.Name></Projects>
    </div>
</template>

<style scoped>

/* Style the tab */
.tab {
  overflow: hidden;
  border: 1px solid #ccc;
  background-color: #f1f1f1;
}

/* Style the buttons inside the tab */
.tab button {
  background-color: inherit;
  float: left;
  border: none;
  outline: none;
  cursor: pointer;
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
  border: 1px solid #ccc;
  border-top: none;
  animation: fadeEffect 1s; /* Fading effect takes 1 second */

}

/* Go from zero to full opacity */
@keyframes fadeEffect {
  from {opacity: 0;}
  to {opacity: 1;}
}
</style>


