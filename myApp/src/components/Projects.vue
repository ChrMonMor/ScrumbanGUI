<script>
import Column from './Columns.vue'
import axios from 'axios';

export default {
  props: {
    ProjectsId: {
      type : Number,
      required: true
    },
    ProjectsName:{
      type : String,
      required: true
    }
  },
  name: 'App',
  components: {
    Column : Column
  },
  data() {
    return {
      cols: null,
      colsURL : "https://localhost:7026/Projects/Columns/"+this.ProjectsId,
      colsNum : null,
    };
  },
  mounted () {
    axios.get(this.colsURL)
      .then(response => (this.cols = response.data, this.colsNum = ((100 / (response.data.length+1)) + "%")))
  }
}
</script>

<template>
  <div class="project">
    <Column v-for="col in cols" :ColumnsId=col.Column_Id :NotNewColumn=true></Column>
    <Column :NotNewColumn=false></Column>
  </div>

</template>

<style>
.column {
  float: left;
  width: v-bind(colsNum);
}
</style>