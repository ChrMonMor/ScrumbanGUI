<script setup>
import Projects from './components/Projects.vue'


</script>
<script>

export default {
  data() {
    return {
      num: 4
    }
  },
}

</script>
<template>
  <Projects
  v-for="n in num"></Projects>
</template>

<style scoped>

</style>
