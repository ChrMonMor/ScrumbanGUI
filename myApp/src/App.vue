<script setup>
import Projects from './components/Projects.vue'
import axios from "axios";

</script>
<script>

export default {
  data() {
    return {
      posts: null,
    };
  },
  mounted () {
    axios
      .get('https://localhost:7026/Projects')
      .then(response => (this.posts = response.data))
  }
}

</script>
<template>
  <div class="project" 
  v-for="n in posts">
    <Projects :ProjectsId=n.Project_Id></Projects>
  </div>
</template>

<style scoped>

</style>
