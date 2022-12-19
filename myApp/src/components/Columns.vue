<script>
import Item from './Items.vue'
import axios from 'axios';

    export default {
        props: {
            projectID:{
                type: Number,
            },
            colNums:{
                type: Number,
            },
            ColumnsId: {
                type: Number,
            },
            NotNewColumn:{
                type: Boolean,
                required: true
            },
            ColumnName:{
                type : String,
            },
            ColPos: {
                type: Number,
            },
            Limit:{
                type: Number,
            }
        },
        name: 'App',
        components: {
            Item: Item
        },
        data() {
            return {
                items: null,
                posMay : 1,
                colNewName :"Placeholder Name",
                showV : false,
                limitColor: '',
            };
        },
        mounted:function() {
            this.refreshData();
        },
        methods:{
            itemURL(){
                if(this.NotNewColumn){
                    return variables.API_URL+"Columns/Items/" + this.ColumnsId;
                }
                return null;
            },
            createNewColumn(){
                axios.get(variables.API_URL+"Columns/Create/" + this.projectID +"/"+this.colNums+"/"+this.colNewName+"/"+"255")
                .then((response) => {
                    this.refreshData();
                    this.items = response.data;
                    this.colNewName = "";
                    document.getElementById("showColEdit").style.display = "none";
                    document.getElementById("showColName").style.display = "block";
                })
            },
            refreshData(){
                axios.get(this.itemURL())
                .then(response => (this.items = response.data, this.posMay = response.data.length+1, this.refreshData()))
            },
            deleteClick(id){
      if(!confirm("Are you sure you want to delete "+ this.ColumnName +"?")){
        return;
      }
                axios.get(variables.API_URL+"Columns/delete/"+id);
            },
            updateClick(id){
                axios.get(variables.API_URL+"Columns/Update/"+id+"/"+this.ColPos+"/"+this.colNewName+"/"+this.Limit)
                .then((response) => {
                    this.refreshData();
                    this.posts = response.data;
                    this.colNewName = "";
                    document.getElementById("showColEdit"+id).style.display = "none";
                    document.getElementById("showColName"+id).style.display = "block";
                })
            },
            updateOptionClick(id){
                if (document.getElementById("showColEdit"+id).style.display = "none") {
                document.getElementById("showColEdit"+id).style.display = "block";
                document.getElementById("showColName"+id).style.display = "none";
                this.colNewName = this.ColumnName;
                document.getElementById("inputColEdit"+id).focus();
                }
            },
        },
        computed:{
            limitColors(){
                if (this.posMay-1 > this.Limit) {
                    this.limitColor = 'red';
                } else if (this.posMay-1 == this.Limit) {
                    this.limitColor = 'green';
                } else {
                    this.limitColor = 'black';
                }
            },
        },
    }

</script>

<template>
    <div v-if=this.NotNewColumn class="columnColumn cyan">
        <div v-bind:id="'showColName'+this.ColumnsId" @click="updateOptionClick(this.ColumnsId)">
            <h3 >{{this.ColumnName}}</h3>
            <p style="text-align: center; font-size: x-small; padding: 0px; margin: 0px; border: 0px; color: v-bind(limitColors());">{{ this.posMay-1 }} / {{ this.Limit }}</p>
        </div>
        <div v-bind:id="'showColEdit'+this.ColumnsId"  style="display: none;">
            <input v-bind:id="'inputColEdit'+this.ColumnsId"  type="text" class="form-control" v-model="colNewName" @focusout="updateClick(this.ColumnsId)" @keyup.enter="updateClick(this.ColumnsId)"/>
        </div>
        <span  id="deleteThis"  class="closeColumn">
            <svg @click=deleteClick(this.ColumnsId) xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x" viewBox="0 0 16 16">
                <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>
            </svg>
        </span >
        <ul class="columnList">
            <li v-for="n in items" :key=n.Item_Id>
                <Item :ItemId=n.Item_Id :ItemColumn=n.Column_Id :ItemPosition=n.Position :ItemName=n.Name :ItemContent=n.Content :NotNewItem=true></Item>
            </li>
            <li><item :maxPosi=this.posMay :ItemColumn=this.ColumnsId :NotNewItem=false ></item></li>
        </ul>
        
    </div>
    <div v-else class="columnColumn orange">
        <button v-bind:id="'showColName'" style="display: block; margin: 0px auto;" @click="updateOptionClick('')">+ Add New Category</button>
        <div v-bind:id="'showColEdit'"  style="display: none;">
            <input v-bind:id="'inputColEdit'"  type="text" class="form-control" v-model="colNewName" @focusout="createNewColumn()" @keyup.enter="createNewColumn()"/>
        </div>
    </div>
</template>

<style>
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

.columnColumn {
  width: v-bind(colsNum);
  word-wrap: break-word;
    border-radius: 8px;
    box-shadow: 0px 15px 20px -10px var(--grayishBlue);
    padding: 8px;
    margin: 4px;  
    background-color: var(--veryLightGray);
  position: relative;
  width: 250px;
  display: inline-block;
}
.columnList {
  list-style-type: none;
  padding: 0;
  margin: 0;
}

.closeColumn {
  position: absolute;
  top: 0;
  right: 0;
  padding: 4px;
}
.bi-x{
  cursor: pointer;
  color: var(--varyDarkBlue);
}
h3 {
    word-wrap: break-word;
    color: var(--varyDark);
    font-weight: var(--weight3);
    padding: 4px;
    text-align: center;
}
</style>