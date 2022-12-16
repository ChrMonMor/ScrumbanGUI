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
      colsURL : variables.API_URL+"Projects/Columns/"+this.ProjectsId,
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
      .then(response => (this.cols = response.data, this.colsNum = ((100 / (response.data.length+1)) + "%"), this.colMax = response.data.length+1, this.refreshData()))
    },
    deleteClick(id){
      if(!confirm("Are you sure you want to delete "+ this.ProjectsName +"?")){
        return;
      }
      axios.get(variables.API_URL+"Projects/delete/"+id);
    }
  }
}
</script>

<template>
  <div>
    <ul class="projectlist">
      <li v-for="col in cols" :key=col.Column_Id >
        <Column :ColPos=col.Position :Limit=col.Limit :ColumnName=col.Name :ColumnsId=col.Column_Id :NotNewColumn=true></Column>
      </li>
      <li><Column :colNums=this.colMax :projectID=this.ProjectsId :NotNewColumn=false ></Column></li>
    </ul>
    
  </div>

</template>

<style scope>

li{
  vertical-align: baseline;
}
.projectlist{
  list-style-type: none;
  padding: 0;
  margin: 0;
  display: flex;
}

</style>