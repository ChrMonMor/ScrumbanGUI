<script>
import Column from './Columns.vue'
import axios from 'axios';

export default {
  props: {
    ProjectsId: {
      type : Number,
      required: true
    },
  },
  name: 'App',
  components: {
    Column : Column
  },
  data() {
    return {
      posts: null
    };
  },
  computed: {
    startUrl(){
      return "https://localhost:7026/Projects/Columns/"+this.ProjectsId;
    }
  },
  mounted () {
    axios.get(startUrl())
      .then(response => (this.posts = response.data))
  }
}

</script>

<template>
    <div class="Column"
    v-for="col in posts">
        <Column></Column>
    </div>
</template>

<style>

</style>