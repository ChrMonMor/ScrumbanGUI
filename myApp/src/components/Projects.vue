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
      myColWid: 1,
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
  },
  computed:{
    colsWidth(){
      return (this.myColWid/this.colMax)*100+'%';
    },
  },
}
</script>

<template>
  <div>
    <ul class="projectlist">
      <li v-for="col in cols" :key=col.Column_Id >
        <Column :ColPos=col.Position :Limit=col.Limit :ColumnName=col.Name :ColumnsId=col.Column_Id :NotNewColumn=true :colNums=this.colMax></Column>
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
  max-width: v-bind(colsWidth);
}
:root {
    --red: hsl(0, 78%, 62%);
    --cyan: hsl(180, 62%, 55%);
    --orange: hsl(34, 97%, 64%);
    --blue: hsl(212, 86%, 64%);
    --varyDarkBlue: hsl(234, 12%, 34%);
    --varyDark: hsl(234, 12%, 24%);
    --grayishBlue: hsl(229, 6%, 66%);
    --veryLightGray: hsl(0, 0%, 96%);
    --veryLight: hsl(0, 0%, 100%);
    --weight1: 200;
    --weight2: 400;
    --weight3: 600;
}

</style>