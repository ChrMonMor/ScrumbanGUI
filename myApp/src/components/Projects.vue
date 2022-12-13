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
      colMax: 0,
    };
  },
  mounted:function () {
    this.refreshData();
  },
  methods:{
    refreshData(){
      axios.get(this.colsURL)
      .then(response => (this.cols = response.data, this.colsNum = ((100 / (response.data.length+1)) + "%"), this.colMax = response.data.length, this.refreshData()))
    },
  }
}
</script>

<template>
  <div class="project">
    <Column v-for="col in cols" :ColumnsId=col.Column_Id :NotNewColumn=true></Column>
    <Column :colNums=this.colMax :projectID=this.ProjectsId :NotNewColumn=false ></Column>
  </div>

</template>

<style>
.column {
  float: left;
  width: v-bind(colsNum);
}
</style>