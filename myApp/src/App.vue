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
      .get('https://localhost:7026/Workers')
      .then(response => (this.posts = response.data))
  }
}

</script>
<template>
  <Projects
  v-for="n in posts"></Projects>
</template>

<style scoped>

</style>
