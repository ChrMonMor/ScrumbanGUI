const project ={template: `<h1>Halllo</h1>`,
props:{
    projectID:{
        type : Number,
        required : true,
    },
},
data(){
    return{
        projectArr:[],
    }
},
methods: {
    refreshData(){
        axios.get(variables.API_URL+"All/"+projectID)
        .then((response) =>{
            this.projectArr=response.data;
        })
    }
},
mounted:function() {
    this.refreshData();
},



}