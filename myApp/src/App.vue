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
      .get('http://localhost:5268/Workers')
      .then(response => (this.posts = response))
  }
}

</script>
<template>
  <div id="app">
  {{ posts }}
</div>
  <Projects
  v-for="n in posts"></Projects>
</template>

<style scoped>

</style>
