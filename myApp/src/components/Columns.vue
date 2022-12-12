<script>
import Item from './Items.vue'
import axios from 'axios';

    export default {
        props: {
            ColumnsId: {
                type: Number,
            },
            NotNewColumn:{
                type: Boolean,
                required: true
            },
        },
        name: 'App',
        components: {
            Item: Item
        },
        data() {
            return {
                items: null,
            };
        },
        mounted() {
            axios.get(this.itemURL())
                .then(response => (this.items = response.data))
        },
        methods:{
            itemURL(){
                if(this.NotNewColumn){
                    return "https://localhost:7026/Columns/Items/" + this.ColumnsId;
                }
                return null;
            }
        }
}
</script>

<template>
    <div v-if=this.NotNewColumn class="column">
        <Item v-for="n in items" :ItemId=n.Item_Id :ItemColumn=n.Column_Id :ItemPosition=n.Position :ItemName=n.Name :ItemContent=n.Content :NotNewItem=true></Item>
        <item :NotNewItem=false></item>
    </div>
    <div v-else class="column">
        <button>+ Add New Category</button>
    </div>
</template>

<style>
.column {
  float: left;
  width: v-bind(colsNum);
}
</style>