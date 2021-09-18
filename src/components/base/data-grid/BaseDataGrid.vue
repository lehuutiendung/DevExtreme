<template>
  <DxDataGrid
    :data-source="dataSource"
    :remote-operations="false"
    :allow-column-reordering="true"
    :allow-column-resizing="true"
    column-resizing-mode="widget"
    :column-auto-width="true"
    :show-borders="true"
    @content-ready="onContentReady"
    height="100%"
    width="100%"
    :hover-state-enabled="true"
  >
    <DxColumn
      v-for="(item, index) in listHeader"
      :key="index"
      :data-field="item.DataField"
      :caption="item.Caption"
      :width="item.Width"
      :fixed="item.Fixed ? item.Fixed : false"
      :alignment="item.Alignment ? item.Alignment : 'left'"
      cell-template="cell-template"
    />
    <template #cell-template="{ data }">
      <slot :name="data.column.dataField" :data="data.data">{{ data.data[data.column.dataField] }}</slot>
    </template>

    <DxGrouping :auto-expand-all="false"/>
    <DxScrolling column-rendering-mode="virtual"/>
    <DxSelection
        :select-all-mode="allMode"
        :show-check-boxes-mode="checkBoxesMode"
        mode="multiple"
      />

    <template #cellTemplate="{ }">
      <div>
          <DxButton icon="edit"/>
      </div>
    </template>
  </DxDataGrid>
</template>
<script>

import {
  DxDataGrid,
  DxColumn,
  DxGrouping,
  DxScrolling,
  DxSelection,
} from 'devextreme-vue/data-grid';
import 'devextreme/data/odata/store';
import DxButton from 'devextreme-vue/button'; 

let collapsed = false;

export default {
  components: {
    DxDataGrid,
    DxColumn,
    DxGrouping,
    DxScrolling,
    DxSelection,
    DxButton,
  },
  props:{
    listHeader:{
      type: Array,
      default: () => {}
    },
    dataSource: {
      type: Array,
      default: () => {}
    }
  },
  data() {
    return {
      onContentReady: function(e) {
        if (!collapsed) {
          e.component.expandRow(['EnviroCare']);
          collapsed = true;
        }
      },
      allMode: 'page',
      checkBoxesMode: 'always',
    };
  },
  methods: {
    test(data){
      console.log(data);
    }
  },
};
</script>

<style scoped>
    @import "../../../css/base/data-grid/base-data-grid.css";
</style>

