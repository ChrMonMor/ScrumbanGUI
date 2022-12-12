<script>
    export default {
        props: {
            ItemId: {
                type: Number,
            },
            ItemColumn:{
                type : Number,
            },
            ItemPosition: {
                type : Number,
            },
            ItemName: {
                type : String,
            },
            ItemContent: {
                type : String,
            },
            NotNewItem: {
                type: Boolean,
                required: true
            }
        }
    }
</script>

<template>
    <div v-if=this.NotNewItem class="card">
        <p>{{this.ItemName}}</p>
        <p>{{this.ItemContent}}</p>
    </div>
    <div v-else class="card">
        <button>+ Add new Card</button>
    </div>
</template>

<style>
.card{
    border: 1px solid black;
    padding: 2px;
    margin: 4px;
}
</style>