<script>
import Column from './Columns.vue'
import axios from 'axios';

export default {
  name: 'App',
  components: {
    Column : Column
  },
  data() {
    return {
      posts: null,
    };
  },
  mounted () {
    axios
      .get('https://localhost:7026/Columns')
      .then(response => (this.posts = response.data))
  }
}

</script>

<template>
    <div class="project">
        <Column></Column>
    </div>
</template>

<style>

</style>